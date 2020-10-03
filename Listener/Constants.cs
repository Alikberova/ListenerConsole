using System.Collections.Generic;
using System.Net;

namespace slsr
{
    //public static class Constants
    //{
    //    public static IPAddress ADenisenko = IPAddress.Parse("192.168.88.84");
    //    public static IPAddress ALavryk = IPAddress.Parse("192.168.88.93");
    //    public static IPAddress BTomak = IPAddress.Parse("192.168.88.79");
    //    public static IPAddress BShyl = IPAddress.Parse("192.168.1.115");
    //    public static IPAddress IDanko = IPAddress.Parse("192.168.88.96");
    //    //public static  IPAddress IDankoOf = IPAddress.Parse("194.28.103.242:5904");
    //    public static IPAddress IMohylnyi = IPAddress.Parse("192.168.88.87");
    //    public static IPAddress OChalyi = IPAddress.Parse("192.168.88.91");
    //    //public static  IPAddress OChalyiOld = IPAddress.Parse("194.28.103.242:5910");
    //    public static IPAddress OStarovoitenko = IPAddress.Parse("192.168.88.90");
    //    public static IPAddress RSkopich = IPAddress.Parse("192.168.88.97");
    //    public static IPAddress SDeryhlazov = IPAddress.Parse("192.168.88.81");
    //    public static IPAddress SHloba = IPAddress.Parse("192.168.88.78");
    //    public static IPAddress VGavrilenko = IPAddress.Parse("192.168.88.85");
    //    public static IPAddress VMakhovka = IPAddress.Parse("176.38.226.73"); //finhya
    //    public static IPAddress Me = IPAddress.Parse("192.168.88.95");
    //}

    public class Constants
    {
        public const int JobEecutionIntervalSec = 20; //todo was 7 // todo change output type

        public Dictionary<string, IPAddress> IPs { get; set; } = new Dictionary<string, IPAddress>()
        {
            { "ADenisenko",  IPAddress.Parse("192.168.88.84") },
            { "ALavryk", IPAddress.Parse("192.168.88.93") },
            { "BTomak", IPAddress.Parse("192.168.88.79") },
            { "BShyl", IPAddress.Parse("192.168.1.115") },
            { "IDanko", IPAddress.Parse("192.168.88.96") },
            { "IMohylnyi", IPAddress.Parse("192.168.88.87") },
            { "OChalyi", IPAddress.Parse("192.168.88.91") },
            { "OStarovoitenko", IPAddress.Parse("192.168.88.90") },
            { "RSkopich", IPAddress.Parse("192.168.88.97") },
            { "SDeryhlazov", IPAddress.Parse("192.168.88.81") },
            { "SHloba", IPAddress.Parse("192.168.88.78") },
            { "VGavrilenko", IPAddress.Parse("92.168.88.85") },
            { "VMakhovka", IPAddress.Parse("176.38.226.73") },
            { "Me", IPAddress.Parse("192.168.88.95") }
        };
    }
}
