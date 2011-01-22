using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;
using System.IO;



namespace PackageService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public int LaunchExportToAcxiom()
        {
            return LaunchPackage(@"sql", "", @"ETL\ExportToAcxiom");
        }

        public int LaunchPackage(string sourceType, string sourceLocation, string packageName)
        {

            string packagePath;
            Package myPackage;
            Application integrationServices = new Application();

            // Combine path and file name.
            packagePath = Path.Combine(sourceLocation, packageName);

            switch (sourceType)
            {
                case "file":
                    // Package is stored as a file.
                    // Add extension if not present.
                    if (String.IsNullOrEmpty(Path.GetExtension(packagePath)))
                    {
                        packagePath = String.Concat(packagePath, ".dtsx");
                    }
                    if (File.Exists(packagePath))
                    {
                        myPackage = integrationServices.LoadPackage(packagePath, null);
                    }
                    else
                    {
                        throw new ApplicationException("Invalid file location: " + packagePath);
                    }
                    break;
                case "sql":
                    // Package is stored in MSDB.
                    // Combine logical path and package name.
                    if (integrationServices.ExistsOnSqlServer(packagePath, @".\sql2008dev", String.Empty, String.Empty))
                    {


                        myPackage = integrationServices.LoadFromSqlServer(packagePath, @".\sql2008dev", String.Empty, String.Empty, null);

                        myPackage.Variables["BatchId"].Value = @"{7DAD4503-0880-4AB3-B803-64FB6BBC25A3}";
                    }
                    else
                    {
                        throw new ApplicationException("Invalid package name or location: " + packagePath);
                    }
                    break;
                case "dts":
                    // Package is managed by SSIS Package Store.
                    // Default logical paths are File System and MSDB.
                    if (integrationServices.ExistsOnDtsServer(packagePath, "."))
                    {
                        myPackage = integrationServices.LoadFromDtsServer(packagePath, "localhost", null);
                    }
                    else
                    {
                        throw new ApplicationException("Invalid package name or location: " + packagePath);
                    }
                    break;
                default:
                    throw new ApplicationException("Invalid sourceType argument: valid values are 'file', 'sql', and 'dts'.");
            }

            var arVariables = Array.CreateInstance(typeof(Variable), myPackage.Variables.Count);
            myPackage.Variables.CopyTo(arVariables, 0);

            var execResult = (Int32)myPackage.Execute();
            return execResult;
        }



    }
}
