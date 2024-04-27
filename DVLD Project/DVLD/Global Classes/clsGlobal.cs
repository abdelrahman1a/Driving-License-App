using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;
using Microsoft.Win32;


namespace DVLD.Classes
{
    internal static  class clsGlobal
    {
        public static clsUser CurrentUser;

       static string valuserName = @"UserName";
        static string valPassName = @"Password";
        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            string KeyPath = @"HKey_LOCAL_MACHINE\SOFTWARE\DVLDProject";
          

            try
            {
                Registry.SetValue(KeyPath, valuserName, Username, RegistryValueKind.String);
                Registry.SetValue(KeyPath, valPassName, Password, RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
                return false;   
            }
              


        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            string KeyPath = @"HKey_LOCAL_MACHINE\SOFTWARE\DVLDProject";

            try
            {
                Username = Registry.GetValue(KeyPath, valuserName , null) as string;
                Password = Registry.GetValue(KeyPath, valPassName , null) as string;

                if (Username != null && Password != null)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }   


        }
    }
}
