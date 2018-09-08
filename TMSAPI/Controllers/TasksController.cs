using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TMSAPI.Models;

namespace TMSAPI.Controllers
{
    public class TasksController : ApiController
    {
        private TMS_DbEntities db = new TMS_DbEntities();
        // GET: api/Tasks
        public List<usp_GetTasks_Result> GetTasks()
        {
            return db.usp_GetTasks().ToList();
        }
        public List<usp_GetTasks_Result> GetMyTask()
        {
            return db.usp_GetTasks().ToList();
        }
        public List<Status> GetStatusList()
        {
            return db.Status.ToList();
        }
        public List<TaskPriority> GetPriorityList()
        {
            return db.TaskPriorities.ToList();
        }
        public List<User> GetAssigneeList()
        {
            return db.Users.ToList();
        }
        public string AddNewTask(CommonView commonView)
        {
                 var AssginerID = from item in db.Users
                                 where item.UserName == commonView.SelectedAssigner
                                 select item.UserID;
                var AssgineeID = from item in db.Users
                                 where item.UserName == commonView.SelectedAssignee
                                 select item.UserID;
                var StatusID = from item in db.Status
                                 where item.StatusName == commonView.SelectedStatus
                                 select item.StatusID;
                var PriorityID = from item in db.TaskPriorities
                                 where item.PriorityName == commonView.SelectedPriority
                                 select item.PriorityID;
                db.usp_InsertTasks(commonView.TaskName, commonView.AssignedDate, commonView.DueDate, StatusID.FirstOrDefault(),PriorityID.FirstOrDefault(),AssgineeID.FirstOrDefault(), AssginerID.FirstOrDefault());
                db.SaveChanges();
                return "success";
        }
        public string EditTask(CommonView commonView)
        {
            var AssginerID = from item in db.Users
                             where item.UserName == commonView.SelectedAssigner
                             select item.UserID;
            var AssgineeID = from item in db.Users
                             where item.UserName == commonView.SelectedAssignee
                             select item.UserID;
            var StatusID = from item in db.Status
                           where item.StatusName == commonView.SelectedStatus
                           select item.StatusID;
            var PriorityID = from item in db.TaskPriorities
                             where item.PriorityName == commonView.SelectedPriority
                             select item.PriorityID;
            db.usp_UpdateTasks(commonView.TaskID,commonView.TaskName, commonView.AssignedDate, commonView.DueDate, StatusID.FirstOrDefault(), PriorityID.FirstOrDefault(), AssgineeID.FirstOrDefault(), AssginerID.FirstOrDefault());
            db.SaveChanges();
            return "success";
        }

        public string RemoveTask(Task task)
        {
            Task Count = db.Tasks.Find(task.TaskID);
            if (Count == null)
            {
                return "fail";
            }

            db.Tasks.Remove(Count);
            db.SaveChanges();

            return "success";
        }
        public usp_GetTasks_Result GetTaskById(int Id)
        {
            usp_GetTasks_Result GetTaskById=null;
             GetTaskById = db.usp_GetTasks().Select(x => x).Where(e => e.TaskID == Id).FirstOrDefault();

            return GetTaskById;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskExists(int id)
        {
            return db.Tasks.Count(e => e.TaskID == id) > 0;
        }
    }
}