using System.Runtime.InteropServices;
namespace MarriageGift.DAO.DAOS
{
    public static class CommonStaticClass
    {
        public static string GetConnectionString()
        {
            var isWindows = System.Runtime.InteropServices.RuntimeInformation
                                               .IsOSPlatform(OSPlatform.Windows);
            if(isWindows)
                return "MarriageGiftDBWin";
            else
                return "MarriageGiftDB";
        }
    }
}
