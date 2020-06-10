using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommonLayer
{
    public static class DTO
    {

        #region User Related DTO
        public class RegisterViewModel
        {
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string UserIdentifire { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string MatricNumber { get; set; }
            public int? StateId { get; set; }
            public int? CityId { get; set; }
            public string Password { get; set; }
            public string PhoneNumber { get; set; }
            public string Role { get; set; }
            public string BusinessName { get; set; }

        }

        public class PDFDTO
        {
            public String MatricNumber { get; set; }
            public PDFListDTO pdflist { get; set; }
        }
        public class PDFListDTO
        {
            public String CourseName { get; set; }
            public String Batch { get; set; }
            public String Grade { get; set; }
        }


        public class LoginViewModel
        {
            public string email { get; set; }
            public string password { get; set; }
        }
        public class UpdatePaswordVM
        {
            public string Currentpassword { get; set; }
            public string NewPossword { get; set; }
            public string Id { get; set; }
        }

        public class FacultyDTO
        {
            public string Name { get; set; }
            public string CreatedDate { get; set; }
            public string GuidKey { get; set; }
        }

        public class StateDTO
        {
            public StateDTO()
            {
                cities = new List<CityDTO>();
            }
            public int Id { get; set; }
            public string Name { get; set; }
            public string GuidKey { get; set; }

            public List<CityDTO> cities { get; set; }
        }

        public class ProgramDTO
        {
            public string Code { get; set; }
            public string Title { get; set; }
            public string DegreeType { get; set; }
            public string GuidKey { get; set; }
            public int Id { get; set; }
            public string AdmissionRequirements { get; set; }
            public int? FacultyId { get; set; }

        }


        public class QuestionListDTO
        {
            public string QuestionCode { get; set; }
            public string QuestionTitle { get; set; }
            public string QuestionType { get; set; }
            public string Answer { get; set; }
            public string Score { get; set; }
            public int QuestionID { get; set; }
        }

        public class AnswerListDTO
        {
            public string AnswerOption { get; set; }
            public int QuestionID { get; set; }
        }
        public class CourseDTO
        {
            public string Code { get; set; }
            public string GuidKey { get; set; }
            public string Title { get; set; }
            public int Semester { get; set; }
            public double? CreditUnit { get; set; }
            public string Level { get; set; }
            public string CourseMaterial { get; set; }
            public string Status { get; set; }
            public string CourseFee { get; set; }
            public string ExamFee { get; set; }
        }

        public class UserLoginWithTokenVM
        {
            public string id { get; set; }
            public RoleVM role { get; set; }
            public string accessToken { get; set; }
            public DateTime tokenExpiry { get; set; }
            public string liscences { get; set; }
        }
        public class CustomerUpdateReqVM
        {
            public string id { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public string confirmPassword { get; set; }
        }

        public class CityDTO
        {
            public string GuidKey { get; set; }
            public int CityId { get; set; }
            public string Name { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
        }
        public class UserVM
        {
            public string id { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public string role { get; set; }
            public string username { get; set; }
        }

        public class RoleVM
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }


        public class ResetlPasswordViewModel
        {
            public string UserName { get; set; }
        }

        public class ResetPasswordViewModel
        {
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
            public string Token { get; set; }



        }

        public class StateAndCityVMDTO
        {
            public int State { get; set; }
            public int City { get; set; }

        }


        public class FileWithCourse
        {
            public string File { get; set; }
            public string CourseCode { get; set; }
        }

        public class EmailConfirmationVM
        {
            public string UserIdentifire { get; set; }
            public string Token { get; set; }
        }

        #endregion

        #region Real Time Notifications & SignalR
        //public class NotificationDTO
        //{
        //    public List<Notification> alerts { get; set; }
        //    public List<Notification> messages { get; set; }
        //    public List<Notification> tasks { get; set; }
        //    public List<Notification> events { get; set; }
        //    public List<Notification> logs { get; set; }
        //    public List<Notification> activities { get; set; }
        //    public int totalUnReadNotifications { get; set; }
        //    public string connectionID { get; set; }
        //    public string data { get; set; }
        //    public string notificationType { get; set; }
        //    public NotificationDTO()
        //    {
        //        alerts = new List<Notification>();
        //        messages = new List<Notification>();
        //        tasks = new List<Notification>();
        //        events = new List<Notification>();
        //        logs = new List<Notification>();
        //        activities = new List<Notification>();
        //    }
        //}
        public class SignalRMessage
        {
            public string connectionID { get; set; }
            public string alertType { get; set; }
            public string sendTo { get; set; }
            public dynamic payload { get; set; }
            public string messageType { get; set; }
            public string userID { get; set; }
        }
        #endregion


        #region Area
        public class AreaDTO
        {
            public virtual int Id { get; set; }
            public virtual string AreaName { get; set; }
        }
        #endregion

        #region Blogs
        public class BlogsDTO
        {
            public virtual int Id { get; set; }
            public virtual string Heading { get; set; }
            public virtual string ImagesUrl { get; set; }
            public virtual string OtherImagesUrl { get; set; }
            public virtual string Description { get; set; }
            public virtual string UserId { get; set; }
            public virtual string UserName { get; set; }
        }
        #endregion

        #region Category 
        public class CategoryDTO
        {
            public virtual int Id { get; set; }
            public virtual string Name { get; set; }
        }
        #endregion

        #region Cuisine
        public class CuisineDTO
        {
            public virtual int Id { get; set; }
            public virtual string Name { get; set; }
        }
        #endregion


        #region SignUpVendorDTO
        public class SignUpVendorVM
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string ProfilePic { get; set; }
            public string StoreName { get; set; }
            public string Website_Url { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public int AreaId { get; set; }
            public int CategoryId { get; set; }
            public int CuisineId { get; set; }
            public string NunberOfLocationName { get; set; }
            public string ConfirmPassword { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public IFormFile SubmitterPicture { get; set; }
            public string OtherArea { get; set; }
            public string OtherCatregory { get; set; }
            public string OtherCusine { get; set; }
            public string AreaIds { get; set; }
            public string CuisineIds { get; set; }
            public int VendorId { get; set; }
        }
        #endregion

        #region SignUpDriver
        public class SignUpDriverVM
        {

            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Address { get; set; }
            public string ConfirmPassword { get; set; }
            public int AreaId { get; set; }
            public string ProfilePic { get; set; }
            public string CarPic { get; set; }
            public string AreaIds { get; set; }

            public string OtherArea { get; set; }
        }
        #endregion


        #region SignUp User
        public class SignUpUserVM
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Address { get; set; }
            public string ConfirmPassword { get; set; }
        }
        #endregion

        #region List Of Resturent By Area Name
        public class ListOfResturantVM
        {
            public int Id { get; set; }
            public string StoreName { get; set; }
            public virtual string Category { get; set; }
            public string Website_Url { get; set; }
            public string Address_Location { get; set; }
            public string UniqueFileName { get; set; }
        }
        #endregion


        #region Edit Vendor VM  public class EditVendorVM
        public class EditVendorVM
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string ProfilePic { get; set; }
            public string StoreName { get; set; }
            public string Website_Url { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public int CategoryId { get; set; }
            public int CuisineId { get; set; }
            public string NunberOfLocationName { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public IFormFile SubmitterPicture { get; set; }
            public string OtherArea { get; set; }
            public string OtherCatregory { get; set; }
            public string OtherCusine { get; set; }
            public int VendorId { get; set; }
        }
        #endregion


        #region Edit Driver
        public class EditDriverVM
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public int AreaId { get; set; }
            public string ProfilePic { get; set; }
            public string CarPic { get; set; }
            public string OtherArea { get; set; }
        }
        #endregion

        #region Edit UserVN
        public class EditUserVM
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
        }

        #endregion

        #region Other LocationVM
        public class OtherLocationVM
        {
            public string LocationName { get; set; }
            public string LocationAddress { get; set; }

            public int VendorID { get; set; }
        }

        #endregion

        #region List Of Other Location 
        public class OtherLocationList
        {
            public string LocationName { get; set; }

            public string LocationAddress { get; set; }

            public int VendorID { get; set; }
        }
        #endregion

        #region Response View Model
        public class ResponseVM
        {
            public string Message { get; set; }
            public bool IsSuccessfull { get; set; }
            public string MessageType { get; set; }
        }
        #endregion


        #region User Profile 
        public class UserProfileVM
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string StoreName { get; set; }
            public string ProfilePic { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string NewPassword { get; set; }
            public string CarPic { get; set; }
            public string ConfirmPassword { get; set; }
            public string PhoneNumber { get; set; }
            public string Password { get; set; }
        }
        public class UserProfilesVM
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string StoreName { get; set; }
            public IFormFile ProfilePic { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string NewPassword { get; set; }
            public IFormFile CarPic { get; set; }
            public string ConfirmPassword { get; set; }
            public string PhoneNumber { get; set; }
            public string Password { get; set; }
        }
        #endregion
    }
}
