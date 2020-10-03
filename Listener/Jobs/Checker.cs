using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace slsr.Jobs
{
    class Checker : IJob
    {
        private const int Port = 5900;
        private const string IsConnected = " is connected";

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                List<TcpConnectionInformation> currentConnections = GetActiveTcpConnections();

                if (currentConnections.Count > 0)
                {
                    var expectedConnections = new Constants();
                    List<TcpConnectionInformation> knownConnections = currentConnections.Where(c => expectedConnections.IPs.Values
                        .Any(address => address.Equals(c.RemoteEndPoint.Address))).ToList();
                    string textMessage = string.Empty;

                    if (knownConnections.Count == 0)
                    {
                        currentConnections.ForEach(c => textMessage += DateTime.Now.ToString() + " " + c.RemoteEndPoint + IsConnected);
                    }
                    else
                    {
                        textMessage = DetermineText(expectedConnections, knownConnections, textMessage);
                    }

                    if (textMessage != string.Empty)
                    {
                        await new Sender().Send(textMessage);
                    }
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        private static string DetermineText(Constants expectedConnections, List<TcpConnectionInformation> knownConnections, string textMessage)
        {
            foreach (TcpConnectionInformation knownConn in knownConnections)
            {
                IPAddress slsr = knownConn.RemoteEndPoint.Address;
                IEnumerable<KeyValuePair<string, IPAddress>> match = expectedConnections.IPs
                    .Where(pair => pair.Value.Equals(slsr));
                textMessage += DateTime.Now.ToString() + " " + match?.SingleOrDefault().Key + IsConnected;
            }

            return textMessage;
        }

        private List<TcpConnectionInformation> GetActiveTcpConnections()
        {
            return IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections()
                .Where(tcp => tcp.RemoteEndPoint.Port == Port).ToList();
        }

        private bool IsVncRunning()
        {
            IPEndPoint[] tcpEndPoints = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners();
            return tcpEndPoints.Any(p => p.Port == Port);
        }

        //private bool IsSomeoneConnected()
        //{
        //    //var lobalProps = IPGlobalStatistics.
        //    TcpConnectionInformation[] tcpConnections = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections();
        //    bool res = tcpConnections.Any(tcpInfo => tcpInfo.LocalEndPoint.Port == port);
        //    return res;
        //}
    }
}
