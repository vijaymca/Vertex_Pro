using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace VertexPortal.Features.Feature1
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("3171b66c-018d-41a1-a9e1-1d83e58aa6a3")]
    public class Feature1EventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {

            SPSite siteCollection = properties.Feature.Parent as SPSite;
            if (siteCollection != null)
            {
                SPWeb topLevelSite = siteCollection.RootWeb;
               // SPGroupCollection collGroups = topLevelSite.SiteGroups;

               // SPUser oUser = topLevelSite.Users[@"SPDOM\Administrator"];
               // SPMember oMember = topLevelSite.Users[@"SPDOM\Administrator"];

               // collGroups.Add("HR", oMember, oUser, "Created using Visual Studio");

                // calculate relative path of site from Web Application root
                string WebAppRelativePath = topLevelSite.ServerRelativeUrl;
                if (!WebAppRelativePath.EndsWith(@"/"))
                {
                    WebAppRelativePath += @"/";
                }

                foreach (SPWeb site in siteCollection.AllWebs)
                {
                    site.MasterUrl = WebAppRelativePath + "_catalogs/masterpage/vertexPortal.master";
                    site.CustomMasterUrl = WebAppRelativePath + "_catalogs/masterpage/vertexPortal.master";
                    site.AlternateCssUrl = WebAppRelativePath + "Style%20Library/CSS/Styles.css";
                    //site.SiteLogoUrl = WebAppRelativePath + "Style%20Library/Branding101/Images/logo.png";

                    SPFolder rootFolder = site.RootFolder;
                    rootFolder.WelcomePage = "SitePages/vhome.aspx";
                    rootFolder.Update();

                    site.UIVersion = 4;
                    site.UIVersionConfigurationEnabled = false;
                    site.Update();
                }




            }
        }
        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPSite siteCollection = properties.Feature.Parent as SPSite;
            if (siteCollection != null)
            {
                SPWeb topLevelSite = siteCollection.RootWeb;

                // calculate relative path of site from Web Application root
                string WebAppRelativePath = topLevelSite.ServerRelativeUrl;
                if (!WebAppRelativePath.EndsWith(@"/"))
                {
                    WebAppRelativePath += @"/";
                }

                foreach (SPWeb site in siteCollection.AllWebs)
                {
                    site.MasterUrl = WebAppRelativePath + "_catalogs/masterpage/v4.master";
                    site.CustomMasterUrl = WebAppRelativePath + "_catalogs/masterpage/v4.master";
                    site.AlternateCssUrl = "";
                    site.SiteLogoUrl = "";
                    site.UIVersion = 4;

                    SPFolder rootFolder = site.RootFolder;
                    rootFolder.WelcomePage = "SitePages/Home.aspx";// Standard default page
                    rootFolder.Update();

                    site.Update();
                }
            }
        }
        //public override void FeatureActivated(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
