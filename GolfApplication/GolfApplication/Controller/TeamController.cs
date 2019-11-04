﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GolfApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GolfApplication.Controller
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]

    public class TeamController : ControllerBase
    {
        #region createTeam
        [HttpPost, Route("createTeam")]
        public IActionResult createTeam(TeamModel team)
        {
            try
            {
                if (team.teamName == "" || team.teamName == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter teamName" } });
                }
                //else if (team.teamIcon == "" || team.teamIcon == null)
                //{
                //    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter teamIcon" } });
                //}
                else if (team.startingHole < 0 || team.startingHole == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter startingHole" } });
                }
                else if (team.createdBy < 0 || team.createdBy == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter createdBy" } });
                }
                else
                {
                    string row = Data.Team.createTeam(team);

                    if (row != "0" || row != null)
                    {
                        return StatusCode((int)HttpStatusCode.OK, "TeamCreated Successfully");
                    }
                    else
                    {
                        //return "Invalid user";
                        return StatusCode((int)HttpStatusCode.Forbidden, new { error = new { message = "Error while creating the Team" } });
                    }
                   
                }
                
            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("createTeam", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
        }
        #endregion

        #region updateTeam
        [HttpPut, Route("updateTeam")]
        public IActionResult updateTeam(updateTeam updateteam)
        {
            updateTeam team = new updateTeam();
            try
            {
                if (updateteam.scoreKeeperID <= 0 || updateteam.scoreKeeperID == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter scoreKeeperID" } });
                }
                else if (updateteam.teamId <= 0 || updateteam.teamId == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter teamId" } });
                }
                else if (updateteam.teamName == "" || updateteam.teamName == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter teamName" } });
                }
                else
                {
                    int row = Data.Team.UpdateTeam(updateteam);

                    if (row > 0)
                    {
                        return StatusCode((int)HttpStatusCode.OK, "Updated Successfully");
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = "Error while Updating the team" } });
                    }
                }
            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("updateTeam", e.Message.ToString());

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message.ToString() } });
            }
        }
        #endregion

        #region deleteTeam
        [HttpDelete, Route("deleteTeam")]
        public IActionResult deleteTeam(int teamId)
        {
            try
            {
                 if (teamId <= 0 || teamId == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter teamId" } });
                }                
                else
                {
                    int row = Data.Team.deleteTeam(teamId);

                    if (row > 0)
                    {
                        return StatusCode((int)HttpStatusCode.OK, "Deleted Successfully");
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = "Error while Deleting the team" } });
                    }
                }
            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("deleteTeam", e.Message.ToString());

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message.ToString() } });
            }
        }
        #endregion

        #region selectTeamById
        [HttpGet, Route("selectTeamById")]
        public IActionResult selectTeamById(int teamId)
        {
            List<getTeam> teamList = new List<getTeam>();
            try
            {
                DataTable dt = Data.Team.selectTeamById(teamId);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        getTeam team = new getTeam();

                        team.teamPlayerListId = (int)dt.Rows[i]["teamPlayerListId"];
                        team.teamId = (int)dt.Rows[i]["teamId"];
                        team.playerName = (dt.Rows[i]["playerName"] == DBNull.Value ? "" : dt.Rows[i]["playerName"].ToString());
                        team.gender = (dt.Rows[i]["gender"] == DBNull.Value ? "" : dt.Rows[i]["gender"].ToString());
                        team.RoleType = (dt.Rows[i]["RoleType"] == DBNull.Value ? "" : dt.Rows[i]["RoleType"].ToString());

                        teamList.Add(team);
                    }
                    return StatusCode((int)HttpStatusCode.OK, teamList);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, teamList);
                }

            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("selectTeamById", e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
        }

        #endregion

        #region listTeam
        [HttpGet, Route("listTeam")]
        public IActionResult listTeam()
        {
            List<updateTeam> teamList = new List<updateTeam>();
            try
            {
                DataTable dt = Data.Team.listTeam();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        updateTeam team = new updateTeam();

                        team.teamId = (int)dt.Rows[i]["teamId"];
                        team.scoreKeeperID = (int)dt.Rows[i]["scoreKeeperID"];
                        team.teamName = (dt.Rows[i]["teamName"] == DBNull.Value ? "" : dt.Rows[i]["teamName"].ToString());
                        team.teamIcon = (dt.Rows[i]["teamIcon"] == DBNull.Value ? "" : dt.Rows[i]["teamIcon"].ToString());
                        team.CreatedOn = (dt.Rows[i]["CreatedOn"] == DBNull.Value ? "" : dt.Rows[i]["CreatedOn"].ToString());
                        team.createdBy = (dt.Rows[i]["createdBy"] == DBNull.Value ? 0 : (int)dt.Rows[i]["createdBy"]);
                        team.startingHole = (dt.Rows[i]["startingHole"] == DBNull.Value ? 0 : (int)dt.Rows[i]["startingHole"]);

                        teamList.Add(team);
                    }
                    return StatusCode((int)HttpStatusCode.OK, teamList);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, teamList);
                }

            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("listTeam", e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
        }
        #endregion

        #region createTeamPlayers
        [HttpPut, Route("createTeamPlayers")]
        public IActionResult createTeamPlayers(TeamPlayer teamPlayer)
        {
            try
            {
                //TeamPlayer teamPlayer
                DataTable dt = Data.Team.createTeamPlayers(teamPlayer);
                string row = dt.Rows[0]["ErrorMessage"].ToString();

                if (dt.Rows[0]["ErrorMessage"].ToString() == "Success")
                {
                    return StatusCode((int)HttpStatusCode.OK, "Team Players created Successfully");
                }
                else
                {
                    //return "Invalid user";
                    return StatusCode((int)HttpStatusCode.Forbidden, new { error = new { message = "Error while creating the Team Players" } });
                }
            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("createTeamPlayers", e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
        }
        #endregion

        #region deleteTeamPlayers
        [HttpDelete, Route("deleteTeamPlayers")]
        public IActionResult deleteTeamPlayers(int teamPlayerListId, int updateBy)
        {
            try
            {
                if (teamPlayerListId <= 0 || teamPlayerListId == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter teamPlayerListId" } });
                }
                else if (updateBy <= 0 || updateBy == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter updated By value" } });
                }
                else
                {
                    int row = Data.Team.deleteTeamPlayers(teamPlayerListId, updateBy);

                    if (row > 0)
                    {
                        return StatusCode((int)HttpStatusCode.OK, "Deleted Successfully");
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = "Error while Deleting the team player" } });
                    }
                }
            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("deleteTeamPlayers", e.Message.ToString());

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message.ToString() } });
            }
        }
        #endregion
    }
}