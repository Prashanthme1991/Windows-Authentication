using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Configuration;

namespace Website17.App_Code
{

public class InitSettings
{
           private  string  sqlConn;
        private  string environment;
        //public  string EnvUrl{get;}
        private string prodhosts;
        private string testhosts;
        

        /// <summary>
        /// sets the environ.
        /// Should be called before any of the methods
        /// </summary>
        /// 
        public InitSettings( )
        {
            //constructor
            //set the value of prod and testhosts from config managaer
            //set prodhosts from config maager
            prodhosts = System.Configuration.ConfigurationManager.AppSettings [ "prodservers" ].ToString( ).ToUpper( );
            testhosts = System.Configuration.ConfigurationManager.AppSettings [ "testservers" ].ToString( ).ToUpper( );
            SetUserSession( );
            //call setEnviron
            SetEnviron( );
        }


        private void SetEnviron( )
        {
            //setting environment  
            string hostname = Dns.GetHostName( ).ToString( ).ToUpper( );
            //if prodhost contains hostname set prod settings
            if ( prodhosts.Contains( hostname ) )
            {
                //prod"
                this.environment = "production";
                this.sqlConn = ConfigurationManager.ConnectionStrings [ "sql4" ].ConnectionString;
            }
            else if ( testhosts.Contains( hostname ) )
            {
                //test"
                this.environment = "test";
                this.sqlConn = ConfigurationManager.ConnectionStrings [ "sql3" ].ConnectionString;
            }
            else
            {
                //dev"
                this.environment = "development";
                this.sqlConn = ConfigurationManager.ConnectionStrings [ "sql3" ].ConnectionString;

            }

        }

        private void SetUserSession()
        {
            string netID = HttpContext.Current.User.Identity.Name.ToString( );
            HttpContext Context = HttpContext.Current;
            Context.Session [ "netID" ] = netID;
           
        }


        public string SqlConn( )
        {
            return sqlConn;
        }

        public string Environment( )
        {
            return environment;
        }

    }
}

