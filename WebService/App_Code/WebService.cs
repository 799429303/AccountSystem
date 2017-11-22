using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DataBase;
using DataBase.DataHelper;

/// <summary>
/// WebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://test.html/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
 [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }
    /// <summary>
    /// 获取网站列表
    /// </summary>
    /// <param name="userID">用户ID</param>
    /// <returns></returns>
    [WebMethod(Description="获取网站列表")]
    public string GetWebSite(string userID)
    {
        IRepositoryBase<AS_website> objMng = new RepositoryBase<AS_website>();
        List<AS_website> lstWebSite = objMng.FindEntities(U => U.userid == userID);
        string strResult = lstWebSite.ToJson();
        return strResult;
        
    }
    /// <summary>
    /// 获取用户名密码
    /// </summary>
    /// <param name="userID">用户ID</param>
    /// <returns></returns>
    [WebMethod(Description = "获取用户名密码")]
    public string GetUserPassword(string userID)
    {
        IRepositoryBase<AS_user_website> objMng = new RepositoryBase<AS_user_website>();
        List<AS_user_website> lstUserWebsite = objMng.FindEntities(U => U.userid == userID);
        string strResult = lstUserWebsite.ToJson();
        return strResult;

    }
    /// <summary>
    /// 获取所有用户
    /// </summary>
    /// <returns></returns>
    [WebMethod(Description = "获取用户")]
    public string GetUsers()
    {
        IRepositoryBase<AS_user> objMng = new RepositoryBase<AS_user>();
        List<AS_user> lstUser = objMng.FindEntities(U => 1==1);
        string strResult = lstUser.ToJson();
        return strResult;

    }
}
