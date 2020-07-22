using System.Runtime.InteropServices;
using PangyaFileCore.Struct;
namespace PangyaFileCore.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Skin
    {
        [field: MarshalAs(UnmanagedType.Struct)]
        public IFFCommon Base {get; set;}
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string MPet { get; set; }
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public byte[] Unk1 { get; set; }

        public uint PriceUnk { get; set; }
        public uint Price7 { get; set; }
        public uint Price30 { get; set; }
    }
}
