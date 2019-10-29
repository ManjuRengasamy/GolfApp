using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GolfApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace GolfApplication.Controller
{
    public class UserController : ControllerBase
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        #region GetUserType
        [HttpGet, Route("UserType")]
        public IActionResult GetUserType()
        {
            List<UserType> userList = new List<UserType>();
            try
            {
                DataTable dt = Data.User.GetUserType();
                if(dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        UserType user = new UserType();

                        user.userTypeId = (int)dt.Rows[i]["userTypeId"];
                        user.userType = (dt.Rows[i]["userType"] == DBNull.Value ? "" : dt.Rows[i]["userType"].ToString());
                        user.description = (dt.Rows[i]["description"] == DBNull.Value ? "" : dt.Rows[i]["description"].ToString());

                        userList.Add(user);
                    }
                        return StatusCode((int)HttpStatusCode.OK, userList);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, userList);
                }

            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
        }
        #endregion
    }
}