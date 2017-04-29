using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Website17.App_Code;

public partial class _Default : System.Web.UI.Page
{
	public void Page_Load( object sender , EventArgs e )
	{
		string netID = HttpContext.Current.User.Identity.Name.ToString( );
        netID = User.Identity.Name;
        lblRole.Text = netID;

        /*
        Response.Write( User.Identity.AuthenticationType );
		HttpContext Context = HttpContext.Current;
		Context.Session [ "netID" ] = netID;*/
        List<string> roles = getRoles( netID );
		lblNetId.Text = netID;
		foreach ( string role in roles )
		{

            lblRole.Text = role;
		}
        RedirectAway( roles[ 0 ] );
	}

	protected bool IsAdmin( string netid )
	{
		//get the list of roles of the user
		//call getRoles passing it the userid
		List<string> roles = getRoles( Session [ "netID" ].ToString( ) );

		//if list contains "admin" return true
		//if roles contains admin then return true
		if ( roles.Contains( "ADMIN" ) )
		{
			return true;
		}
		else
		{
			return false;
		}



	}

	/// <summary>
	/// returns true if the user is viewer
	/// </summary>
	/// <param name="netid"> neid of user to search role</param>
	/// <returns> true if user is viewer </returns>
	protected bool IsViewer( string netid )
	{
		//get the list of roles of the user


		//call getRoles passing it the userid
		List<string> roles =getRoles( Session [ "netID" ].ToString( ) );

		//if list contains "viewer" return true
		if ( roles.Contains( "VIEWER" ) )
		{
			return true;
		}
		else
		{
			return false;
		}


	}

	/// <summary>
	/// returns true if the user is staff
	/// </summary>
	/// <param name="netid"> neid of user to search role</param>
	/// <returns> true if user is staff </returns>
	protected bool IsStaff( string netid )
	{
		//get the list of roles of the user


		//call getRoles passing it the userid
		List<string> roles = getRoles( Session [ "netID" ].ToString( ) );

		//if list contains "staff" return true
		if ( roles.Contains( "STAFF" ) )
		{
			return true;
		}
		else
			return false;
	}



	/// <summary>
	/// gets the list of roles of the user with netid
	/// </summary>
	/// <param name="netid"> netid of user to return roles</param>
	/// <returns> list of roles the user has</returns>
	private List<string> getRoles( string netid )
	{
		List<string> roles = new List<string>( );

		using ( SqlConnection myCon = new SqlConnection( ( new InitSettings( ) ).SqlConn( ) ) )
		{

			myCon.Open( );
			SqlCommand searchCmd = new SqlCommand( "Select role_acronym from dbo.myschedule_view where netID='" + netid + "';" , myCon );
			searchCmd.Connection = myCon;
			SqlDataReader dr = searchCmd.ExecuteReader( );
			while ( dr.Read( ) )
			{
				roles.Add( dr.GetString( 0 ).ToString( ) );
			}
		}
		return roles;
	}

	//you can write redirect method for (admin/staff/view) that should be called for protection. e.g 

    protected void RedirectAway( string role )
    {
        switch ( role )
        {
            case "ADMIN":
                Response.Redirect( "admin.aspx" );
                break;
            case "STAFF":
                Response.Redirect( "staff.aspx" );
                break;
            case "VIEWER":
                Response.Redirect( "viewer.aspx" );
                break;
            case "DEFAULT":
                Response.Redirect( "viewer.aspx" );
                break;

        }
    }
	


}