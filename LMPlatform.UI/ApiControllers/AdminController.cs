﻿using Application.Core;
using Application.Infrastructure.GroupManagement;
using Application.Infrastructure.LecturerManagement;
using Application.Infrastructure.StudentManagement;
using Application.Infrastructure.SubjectManagement;
using Application.Infrastructure.UserManagement;
using LMPlatform.UI.ViewModels.AccountViewModels;
using LMPlatform.UI.ViewModels.AdministrationViewModels;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Security;

namespace LMPlatform.UI.ApiControllers
{
	[RoutePrefix("[controller]")]
    public class AdminController : ApiController
    {
		#region Dependencies

		public LazyDependency<IStudentManagementService> StudentManagementService => new LazyDependency<IStudentManagementService>();

		public LazyDependency<ISubjectManagementService> SubjectManagementService => new LazyDependency<ISubjectManagementService>();

		public LazyDependency<IGroupManagementService> GroupManagementService => new LazyDependency<IGroupManagementService>();

		public LazyDependency<ILecturerManagementService> LecturerManagementService => new LazyDependency<ILecturerManagementService>();

	    public LazyDependency<IUsersManagementService> UsersManagementService => new LazyDependency<IUsersManagementService>();

		#endregion

		#region TODO

		[HttpGet]
		public HttpResponseMessage UserActivity()
		{
			try
			{
				var activityModel = new UserActivityViewModel();

				return new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new ObjectContent<UserActivityViewModel>(activityModel, new JsonMediaTypeFormatter())
				};
			}
			catch (Exception e)
			{
				return new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
					Content = new StringContent(e.Message)
				};
			}
		}

		[HttpGet]
		public HttpResponseMessage Students()
		{
			try
			{
				var students = StudentManagementService.Value
					.GetStudents()
					.Select(s =>
						new
						{
							s.Confirmed,
							s.Email,
							s.FirstName,
							s.FullName,
							s.GroupId,
							s.LastName,
							s.MiddleName,
							s.Id,
							s.IsNew
						});

				return new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new ObjectContent(students.GetType(), students, new JsonMediaTypeFormatter())
				};
			}
			catch (Exception e)
			{
				return new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
					Content = new StringContent(e.Message)
				};
			}
		}

		//public ActionResult EditStudent(int id)
		//{
		//	var student = StudentManagementService.GetStudent(id);

		//	if (student != null)
		//	{
		//		var model = new ModifyStudentViewModel(student);
		//		return PartialView("_EditStudentView", model);
		//	}

		//	return RedirectToAction("Index");
		//}

		//[System.Web.Mvc.HttpPost]
		//public ActionResult EditStudent(ModifyStudentViewModel model, int id)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		try
		//		{
		//			model.ModifyStudent();
		//			ViewBag.ResultSuccess = true;
		//		}
		//		catch
		//		{
		//			ModelState.AddModelError(string.Empty, string.Empty);
		//		}
		//	}

		//	return null;
		//}

		//[System.Web.Mvc.HttpPost]
		//public ActionResult EditStudentAjax(ModifyStudentViewModel model)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		try
		//		{
		//			var user = UsersManagementService.GetUserByName(model.Name, model.Surname, model.Patronymic);
		//			if (user == null || user.Id == model.Id)
		//			{
		//				model.ModifyStudent();
		//				return Json(new { resultMessage = "Студент сохранен" });
		//			}

		//			ModelState.AddModelError(string.Empty, "Пользователь с таким именем уже существует");
		//		}
		//		catch
		//		{
		//			ModelState.AddModelError(string.Empty, string.Empty);
		//		}
		//	}

		//	return PartialView("_EditStudentView", model);
		//}

		//public ActionResult AddProfessor()
		//{
		//	var model = new RegisterViewModel();
		//	return PartialView("_AddProfessorView", model);
		//}

		//[System.Web.Mvc.HttpPost]
		//public ActionResult AddProfessorAjax(RegisterViewModel model)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		try
		//		{
		//			var user = UsersManagementService.GetUserByName(model.Name, model.Surname, model.Patronymic);
		//			if (user == null)
		//			{
		//				model.RegistrationUser(new[] { "lector" });
		//				return Json(new { resultMessage = "Преподаватель сохранен" });
		//			}

		//			ModelState.AddModelError(string.Empty, "Пользователь с таким именем уже существует");
		//		}
		//		catch (MembershipCreateUserException e)
		//		{
		//			ModelState.AddModelError(string.Empty, e.StatusCode.ToString());
		//		}
		//	}

		//	return PartialView("_AddProfessorView", model);
		//}

		[HttpPost]
		public HttpResponseMessage AddProfessor(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					model.RegistrationUser(new[] { "lector" });
					return new HttpResponseMessage(HttpStatusCode.OK);
				}
				catch (MembershipCreateUserException e)
				{
					return new HttpResponseMessage(HttpStatusCode.InternalServerError)
					{
						Content = new ObjectContent<string>(e.StatusCode.ToString(), new JsonMediaTypeFormatter())
					};
				}
			}

			return new HttpResponseMessage(HttpStatusCode.BadRequest);
		}

		//public ActionResult EditProfessor(int id)
		//{
		//	var lecturer = LecturerManagementService.GetLecturer(id);

		//	if (lecturer != null)
		//	{
		//		var model = new ModifyLecturerViewModel(lecturer);
		//		return PartialView("_EditProfessorView", model);
		//	}

		//	return RedirectToAction("Index");
		//}

		//[System.Web.Mvc.HttpPost]
		//public ActionResult EditProfessor(ModifyLecturerViewModel model)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		try
		//		{
		//			model.ModifyLecturer();
		//			ViewBag.ResultSuccess = true;
		//		}
		//		catch
		//		{
		//			ModelState.AddModelError(string.Empty, string.Empty);
		//		}
		//	}

		//	return null;
		//}

		//[System.Web.Mvc.HttpPost]
		//public ActionResult EditProfessorAjax(ModifyLecturerViewModel model)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		try
		//		{
		//			var user = UsersManagementService.GetUserByName(model.Name, model.Surname, model.Patronymic);
		//			if (user == null || user.Id == model.LecturerId)
		//			{
		//				model.ModifyLecturer();
		//				return this.Json(new { resultMessage = "Преподаватель сохранен" });
		//			}

		//			ModelState.AddModelError(string.Empty, "Пользователь с таким именем уже существует");
		//		}
		//		catch
		//		{
		//			ModelState.AddModelError(string.Empty, string.Empty);
		//		}
		//	}

		//	return PartialView("_EditProfessorView", model);
		//}

		//public ActionResult AddGroup()
		//{
		//	var model = new GroupViewModel();
		//	return PartialView("_AddGroupView", model);
		//}

		//[System.Web.Mvc.HttpPost]
		//public ActionResult AddGroup(GroupViewModel model)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		try
		//		{
		//			if (!model.CheckGroupName())
		//			{
		//				ModelState.AddModelError(string.Empty, "Группа с таким номером уже существует");
		//			}
		//			else
		//			{
		//				model.AddGroup();
		//			}
		//		}
		//		catch (MembershipCreateUserException e)
		//		{
		//			ModelState.AddModelError(string.Empty, e.StatusCode.ToString());
		//		}
		//	}

		//	return View(model);
		//}

		//[System.Web.Mvc.HttpPost]
		//public ActionResult AddGroupAjax(GroupViewModel model)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		try
		//		{
		//			if (!model.CheckGroupName())
		//			{
		//				ModelState.AddModelError(string.Empty, "Группа с таким номером уже существует");
		//			}
		//			else
		//			{
		//				model.AddGroup();
		//				return Json(new { resultMessage = "Группа сохранена" });
		//			}
		//		}
		//		catch (MembershipCreateUserException e)
		//		{
		//			ModelState.AddModelError(string.Empty, e.StatusCode.ToString());
		//		}
		//	}

		//	return PartialView("_AddGroupView", model);
		//}

		//public ActionResult EditGroup(int id)
		//{
		//	var group = GroupManagementService.GetGroup(id);

		//	if (group != null)
		//	{
		//		var model = new GroupViewModel(group);
		//		return PartialView("_EditGroupView", model);
		//	}

		//	return RedirectToAction("Index");
		//}

		[HttpPost]
		public HttpResponseMessage EditGroup(GroupViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					model.ModifyGroup();
					return new HttpResponseMessage(HttpStatusCode.OK);
				}
				catch
				{
					return new HttpResponseMessage(HttpStatusCode.InternalServerError);
				}
			}

			return new HttpResponseMessage(HttpStatusCode.BadRequest);
		}

		//[System.Web.Mvc.HttpPost]
		//public ActionResult EditGroupAjax(GroupViewModel model)
		//{
		//	if (!model.CheckGroupName())
		//	{
		//		this.ModelState.AddModelError(string.Empty, "Группа с таким именем уже существует");
		//	}

		//	if (ModelState.IsValid && model.CheckGroupName())
		//	{
		//		try
		//		{
		//			if (!model.CheckGroupName())
		//			{
		//				this.ModelState.AddModelError(string.Empty, "Группа с таким именем уже существует");
		//			}
		//			else
		//			{
		//				model.ModifyGroup();
		//				return Json(new { resultMessage = "Группа сохранена" });
		//			}
		//		}
		//		catch
		//		{
		//			ModelState.AddModelError(string.Empty, "Произошла ошибка");
		//		}
		//	}

		//	return PartialView("_EditGroupView", model);
		//}

		//public ActionResult Professors()
		//{
		//	return View();
		//}

		[HttpGet]
		public HttpResponseMessage Groups()
		{
			try
			{
				var groups = GroupManagementService.Value.GetGroups().Select(g => new
				{
					g.Id,
					g.IsNew,
					g.StartYear,
					g.GraduationYear,
					g.Name,
					g.SecretaryId
				});
				
				return new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new ObjectContent(groups.GetType(), groups, new JsonMediaTypeFormatter())
				};
			}
			catch (Exception e)
			{
				return new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
					Content = new ObjectContent(e.GetType(), e, new JsonMediaTypeFormatter())
				};
			}
		}

		[HttpGet]
		public HttpResponseMessage ListOfStudents([FromUri] int id)
		{
			try
			{
				var students = StudentManagementService.Value
					.GetGroupStudents(id)
					.OrderBy(student => student.FullName)
					.Select(s =>
						new
						{
							s.Confirmed,
							s.Email,
							s.FirstName,
							s.FullName,
							s.GroupId,
							s.LastName,
							s.MiddleName,
							s.Id,
							s.IsNew
						});

				if (students.Any())
				{
					return new HttpResponseMessage(HttpStatusCode.OK)
					{
						Content = new ObjectContent(students.GetType(), students, new JsonMediaTypeFormatter())
					};
				}
			}
			catch (Exception e)
			{
				return new HttpResponseMessage(HttpStatusCode.InternalServerError);
			}

			return new HttpResponseMessage(HttpStatusCode.BadRequest);
		}

		//public ActionResult ListOfGroups(int id)
		//{
		//	var sub = SubjectManagementService.GetSubjectsByLector(id).OrderBy(subject => subject.Name).ToList();
		//	var lec = LecturerManagementService.GetLecturer(id);
		//	if (sub != null)
		//	{

		//		var model = ListSubjectViewModel.FormSubjects(sub, lec.FullName);
		//		return PartialView("_ListOfGroups", model);
		//	}

		//	return RedirectToAction("Index");
		//}

		//public ActionResult ListOfSubject(int id)
		//{
		//	var groups = SubjectManagementService.GetSubjectsByStudent(id).OrderBy(subject => subject.Name).ToList();
		//	var stud = StudentManagementService.GetStudent(id);

		//	if (groups != null)
		//	{
		//		var model = ListSubjectViewModel.FormSubjects(groups, stud.FullName);
		//		return PartialView("ListOfSubject", model);
		//	}

		//	return RedirectToAction("Index");
		//}


		//public ActionResult Profile(int id)
		//{
		//	var login = UsersManagementService.GetUser(id);

		//	if (login != null)
		//	{

		//		//var model =  StudentViewModel.FromStudent();
		//		return Redirect(string.Format("/Profile/Page/{0}", login.UserName));
		//	}

		//	return RedirectToAction("Index");
		//}

		//public ActionResult Files()
		//{
		//	return View();
		//}

		//public ActionResult ResetPassword(int id)
		//{
		//	try
		//	{
		//		var user = UsersManagementService.GetUser(id);


		//		var resetPassModel = new ResetPasswordViewModel(user);
		//		if (Request != null && Request.UrlReferrer != null)
		//		{
		//			ViewBag.ReturnUrl = Request.UrlReferrer;
		//		}
		//		else
		//		{
		//			ViewBag.ReturnUrl = "/Administration/Index";
		//		}

		//		return View(resetPassModel);
		//	}
		//	catch (InvalidOperationException)
		//	{
		//		return RedirectToAction("Index");
		//	}
		//}

		//[System.Web.Mvc.HttpPost]
		//public ActionResult ResetPassword(ResetPasswordViewModel model)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		var resetResult = model.ResetPassword();
		//		ViewBag.ResultSuccess = resetResult;

		//		if (!resetResult)
		//		{
		//			ModelState.AddModelError(string.Empty, "Пароль не был сброшен");
		//		}
		//	}

		//	return View(model);
		//}

		[System.Web.Mvc.HttpPost]
		public HttpResponseMessage DeleteUser(int id)
		{
			try
			{
				var isDeleted = UsersManagementService.Value.DeleteUser(id);

				return new HttpResponseMessage(isDeleted ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
			}
			catch
			{
				return new HttpResponseMessage(HttpStatusCode.InternalServerError);
			}
		}

		//[System.Web.Mvc.HttpPost]
		//public JsonResult DeleteStudent(int id)
		//{
		//	try
		//	{
		//		var student = StudentManagementService.GetStudent(id);
		//		if (student != null)
		//		{
		//			var result = StudentManagementService.DeleteStudent(id);
		//			if (result)
		//			{
		//				return Json(new { resultMessage = string.Format("Студент {0} удален", student.FullName) });
		//			}

		//			return Json(new { resultMessage = string.Format("Не удалось удалить студента {0}", student.FullName) });
		//		}

		//		return Json(new { resultMessage = "Удаление невозможно. Студента не существует", status = "500" });
		//	}
		//	catch (Exception ex)
		//	{
		//		return Json(new { resultMessage = ex.Message, status = "500" });
		//	}
		//}

		//[System.Web.Mvc.HttpPost]
		//public JsonResult DeleteLecturer(int id)
		//{
		//	try
		//	{
		//		var lecturer = LecturerManagementService.GetLecturer(id);

		//		if (lecturer != null)
		//		{
		//			if (lecturer.SubjectLecturers != null && lecturer.SubjectLecturers.Any() && lecturer.SubjectLecturers.All(e => e.Subject.IsArchive))
		//			{
		//				foreach (var lecturerSubjectLecturer in lecturer.SubjectLecturers)
		//				{
		//					LecturerManagementService.DisjoinOwnerLector(lecturerSubjectLecturer.SubjectId, id);
		//				}
		//			}
		//			//else if (lecturer.SubjectLecturers != null && lecturer.SubjectLecturers.Any() && lecturer.SubjectLecturers.Any(e => !e.Subject.IsArchive))
		//			//{
		//			//	return Json(new { resultMessage = "Удаление невозможно. Преподаватель связан с предметами", status = "500" });
		//			//}

		//			var result = LecturerManagementService.DeleteLecturer(id);

		//			if (result)
		//			{
		//				return Json(new { resultMessage = string.Format("Преподаватель {0} удален", lecturer.FullName) });
		//			}

		//			return Json(new { resultMessage = string.Format("Не удалось удалить преподавателя {0}", lecturer.FullName), status = "500" });
		//		}

		//		return Json(new { resultMessage = "Удаление невозможно. Преподавателя не существует", status = "500" });
		//	}
		//	catch
		//	{
		//		return Json(new { resultMessage = "Произошла ошибка при удалении", status = "500" });
		//	}
		//}

		//[System.Web.Mvc.HttpPost]
		//public JsonResult DeleteGroup(int id)
		//{
		//	try
		//	{
		//		var group = GroupManagementService.GetGroup(id);
		//		if (group != null)
		//		{
		//			if (group.Students != null && group.Students.Count > 0)
		//			{
		//				return Json(new { resultMessage = "Группа содержит студентов и не может быть удалена" });
		//			}

		//			GroupManagementService.DeleteGroup(id);
		//			return Json(new { resultMessage = string.Format("Группа {0} удалена", group.Name) });
		//		}

		//		return Json(new { resultMessage = "Группы не существует", status = "500" });
		//	}
		//	catch (Exception ex)
		//	{
		//		return Json(new { resultMessage = ex.Message, status = "500" });
		//	}
		//}

		[HttpGet]
		public HttpResponseMessage Attendance(int id)
		{
			var user = UsersManagementService.Value.GetUser(id);

			if (user?.Attendance != null)
			{
				var data = user.AttendanceList.GroupBy(e => e.Date).Select(d => new { day = d.Key.ToString("d"), count = d.Count() });
				return new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new ObjectContent<object>(new { resultMessage = user.FullName, attendance = data }, new JsonMediaTypeFormatter())
				};
			}

			return new HttpResponseMessage(HttpStatusCode.BadRequest);
		}

		//[System.Web.Mvc.HttpPost]
		//public DataTablesResult<StudentViewModel> GetCollectionStudents(DataTablesParam dataTableParam)
		//{
		//	var searchString = dataTableParam.GetSearchString();
		//	ViewBag.Profile = "/Administration/Profile";
		//	ViewBag.ListOfSubject = "/Administration/ListOfSubject";
		//	ViewBag.EditActionLink = "/Administration/EditStudent";
		//	ViewBag.DeleteActionLink = "/Administration/DeleteStudent";
		//	ViewBag.StatActionLink = "/Administration/Attendance";
		//	var students = StudentManagementService.GetStudentsPageable(pageInfo: dataTableParam.ToPageInfo(), searchString: searchString);
		//	this.SetupSettings(dataTableParam);
		//	return DataTableExtensions.GetResults(students.Items.Select(s => StudentViewModel.FromStudent(s, PartialViewToString("_EditGlyphLinks", s.Id))), dataTableParam, students.TotalCount);
		//}

		//[System.Web.Mvc.HttpPost]
		//public DataTablesResult<LecturerViewModel> GetCollectionLecturers(DataTablesParam dataTableParam)
		//{
		//	var searchString = dataTableParam.GetSearchString();
		//	ViewBag.Profile = "/Administration/Profile";
		//	ViewBag.ListOfSubject = "/Administration/ListOfGroups";
		//	ViewBag.EditActionLink = "/Administration/EditProfessor";
		//	ViewBag.DeleteActionLink = "/Administration/DeleteLecturer";
		//	ViewBag.StatActionLink = "/Administration/Attendance";
		//	var lecturers = LecturerManagementService.GetLecturersPageable(pageInfo: dataTableParam.ToPageInfo(), searchString: searchString);
		//	this.SetupSettings(dataTableParam);
		//	return DataTableExtensions.GetResults(lecturers.Items.Select(l => LecturerViewModel.FormLecturers(l, PartialViewToString("_EditGlyphLinks", l.Id, l.IsActive))), dataTableParam, lecturers.TotalCount);
		//}

		//[System.Web.Mvc.HttpPost]
		//public DataTablesResult<GroupViewModel> GetCollectionGroups(DataTablesParam dataTableParam)
		//{
		//	var searchString = dataTableParam.GetSearchString();
		//	ViewBag.ListOfStudents = "/Administration/ListOfStudents";
		//	ViewBag.EditActionLink = "/Administration/EditGroup";
		//	ViewBag.DeleteActionLink = "/Administration/DeleteGroup";
		//	var groups = GroupManagementService.GetGroupsPageable(pageInfo: dataTableParam.ToPageInfo(), searchString: searchString);
		//	this.SetupSettings(dataTableParam);
		//	return DataTableExtensions.GetResults(groups.Items.Select(g => GroupViewModel.FormGroup(g, PartialViewToString("_EditGlyphLinks", g.Id))), dataTableParam, groups.TotalCount);
		//}
		//private void SetupSettings(DataTablesParam dataTableParam)
		//{
		//	var n = 20;

		//	for (var i = 0; i < n; i++)
		//	{
		//		if (string.IsNullOrEmpty(this.HttpContext.Request.Form["iSortCol_" + i]))
		//		{
		//			return;
		//		}

		//		dataTableParam.iSortCol.Add(int.Parse(this.HttpContext.Request.Form["iSortCol_" + i]));
		//		dataTableParam.sSortDir.Add(this.HttpContext.Request.Form["sSortDir_" + i]);
		//	}

		//}


		#endregion
    }
}