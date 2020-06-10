using BusinessLayer.Utilities;
using CommonLayer;
using DataAccessLayer;
using DataAccessLayer.InterfacesRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using static CommonLayer.DTO;
using static CommonLayer.Enums;
using static CommonLayer.Helper.Utils;

namespace BusinessLayer
{
    public class UserBL : BusinessBase<AppUser>
    {
        private GrubNowDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private BusinessWrapper _businessWrapper;
        private readonly RoleManager<AppRole> _roleManager;
        private IHttpContextAccessor httpContext;
        private IWebHostEnvironment _environment;
        private BaseResponse response;
        private IEfRepository _efRepository;
        private IListOfAllData _listOfAll;
        public UserBL(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            _context = serviceProvider.GetRequiredService<GrubNowDbContext>();
            _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            _signInManager = serviceProvider.GetRequiredService<SignInManager<AppUser>>();
            _businessWrapper = serviceProvider.GetRequiredService<BusinessWrapper>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            _environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            _efRepository = serviceProvider.GetRequiredService<IEfRepository>();
            _listOfAll = serviceProvider.GetRequiredService<IListOfAllData>();
            response = new BaseResponse();
        }
        public async Task<bool> SignUp(RegisterViewModel model)
        {
            bool isSuccessful = false;
            var user = new AppUser
            {
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FirstName = model.FirstName,
                UserName = model.Email,
            };
            var usr = await _userManager.FindByEmailAsync(model.Email);
            if (usr != null)
            {
                OtherConstants.responseMsg = "User already exists!";
                OtherConstants.messageType = MessageType.Error;
                isSuccessful = false;
            }
            else
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var result1 = await _userManager.AddToRoleAsync(user, model.Role);
                    if (result1.Succeeded)
                    {
                        // Email Confirmation Link To User
                        var userData = _userManager.FindByNameAsync(user.UserName).Result;

                        //Token email
                        string emailConfrmToken = _userManager
                                                  .GenerateEmailConfirmationTokenAsync(userData)
                                                  .Result;

                        // Genrate Reset Link for Password 
                        string resetLink = "https://localhost:4400/Account/EmailConfirmation/?UserIdentifire=" + userData.Id + "/?token=" + emailConfrmToken;

                        //Its Dummy
                        var toEmail = "alt.bm-e02xa4p@yopmail.com";
                        var emailSubject = "Email Confirmation To Activate Account";
                        var fromEmailAddress = "alt.bm-e02xa4p@yopmail.com";
                        var emailBody = "Hello User.<br/> Welcome to Plenum. <br/> Your Email Confirmation link is Here  " + "<br/>" + " You can " + "<a href=" + resetLink + ">Click Here</a>" + " to activate your account. <br/> Thanks.";
                        response.dynamicResult = sendMail(toEmail, emailSubject, emailBody, fromEmailAddress);


                        OtherConstants.responseMsg = "User registered successfully.Please Check Your Email to Verify Your Account";
                        OtherConstants.messageType = MessageType.Success;
                        isSuccessful = true;
                    }
                }
                else
                {
                    OtherConstants.messageType = MessageType.Error;
                    isSuccessful = false;
                    if (result.Errors.Count() > 0)
                    {
                        foreach (var error in result.Errors)
                        {
                            OtherConstants.responseMsg += error.Description + "\n";
                        }
                    }
                    else
                        OtherConstants.responseMsg = "User registration failed!";
                }
            }
            return isSuccessful;
        }
        public async Task<string> UpdatePassword(UpdatePaswordVM model)
        {
            
            string Message = string.Empty;
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user!=null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.Currentpassword, model.NewPossword);
                if (result.Succeeded)
                {
                    Message = "Update Password Successfully";
                    
                    OtherConstants.messageType = MessageType.Success;
                    OtherConstants.isSuccessful = true;
                    OtherConstants.responseMsg = Message;
                    return null;
                }
                else
                {
                    Message = "Please Check Current Password";
                    OtherConstants.messageType = MessageType.Error;
                    OtherConstants.isSuccessful = false;
                    OtherConstants.responseMsg = Message;
                    return null;
                }
            }
            else
            {
                Message = "User Does Not Exist";
                OtherConstants.messageType = MessageType.Error;
                OtherConstants.isSuccessful = false;
                OtherConstants.responseMsg = Message;
                return null;
            }
            
        }

        // This methods Generate Token And Send Email To user 
        public async Task<string> ResetPasswordLink(ResetlPasswordViewModel model)
        {
            string Message = string.Empty;
            var user = _userManager.FindByNameAsync(model.UserName).Result;
            if (user == null || !(_userManager.IsEmailConfirmedAsync(user).Result))
            {
                Message = "Email Not Confirmed";
                OtherConstants.messageType = MessageType.Error;
                OtherConstants.isSuccessful = false;
                OtherConstants.responseMsg = Message;
                return null;
                
            }

            // Genrate password Reset Token 
            var token = _userManager.
                  GeneratePasswordResetTokenAsync(user).Result;


            //_context.TokenForResetPassword.Add(tokenFor);
            await _context.SaveChangesAsync();


            // Genrate Reset Link for Password 
            string resetLink = "https://localhost:4400/ResetPassword/Account/?token=" + token;

            //Its Dummy
            var toEmail = "grubnow@yopmail.com";
            var emailSubject = "Forget Password Link";
            var fromEmailAddress = "grubnow@yopmail.com";
            var emailBody = "Hello User.<br/> Welcome to GrubNow. <br/> Your Password reset link is Here  " + "<br/>" + " You can " + "<a href=" + resetLink + ">Click Here</a>" + " to change your password and activate your account. <br/> Thanks.  <br/> Token="+token;
            Message = sendMail(toEmail, emailSubject, emailBody, fromEmailAddress);
            TokenForResetPassword reset = new TokenForResetPassword();
            reset.Token = token;
            reset.UserId = user.Id;
            _context.TokenForResetPasswords.Add(reset);
            _context.SaveChanges();
            OtherConstants.messageType = MessageType.Success;
            OtherConstants.isSuccessful = true;
            OtherConstants.responseMsg = Message;
            return null;
        }

        // Token And Other Informayion Sent To this Methods and reset password
        public async Task<string> ResetPassword(ResetPasswordViewModel model)
        {
            string Message = string.Empty;
            var UserId = _context.TokenForResetPasswords.FirstOrDefault(x => x.Token == model.Token);
            if (UserId != null)
            {
                var user = await _userManager.FindByIdAsync(UserId.UserId);
                var result = _userManager.ResetPasswordAsync
                          (user, model.Token, model.Password).Result;
                if (result.Succeeded)
                {
                    Message = "Password Reset Successfully";
                    OtherConstants.messageType = MessageType.Success;
                    OtherConstants.isSuccessful = true;
                    OtherConstants.responseMsg = Message;
                    return null;

                }
                else
                {
                    Message = "Error While Reset Password";
                    OtherConstants.messageType = MessageType.Error;
                    OtherConstants.isSuccessful = false;
                    OtherConstants.responseMsg = Message;
                    return null;

                }
            }
            else
            {
                Message = "Token Invalid";
                OtherConstants.messageType = MessageType.Error;
                OtherConstants.isSuccessful = false;
                OtherConstants.responseMsg = Message;
                return null;
            }
        }

        public async Task<UserLoginWithTokenVM> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var role = await _userManager.GetRolesAsync(user);
            UserLoginWithTokenVM customer = new UserLoginWithTokenVM();
            //var customer = mapper.Map<CustomersVM>(user);
            // customer.role = role[0];
            return customer;
        }
        public async Task<bool> Put(string id, UserVM payload)
        {
            bool isSuccessful = false;
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = payload.email;
                user.UserName = payload.username;

                var result = await _userManager.UpdateAsync(user);
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles.ToArray());

                await _userManager.AddToRoleAsync(user, payload.role);

                if (result.Succeeded)
                {
                    isSuccessful = true;
                    OtherConstants.responseMsg = "User updated successfully!";
                    OtherConstants.messageType = MessageType.Success;
                }
                else
                {
                    OtherConstants.messageType = MessageType.Error;
                    isSuccessful = false;
                    OtherConstants.responseMsg = "User could not be updated!";
                }
            }
            return isSuccessful;
        }


        #region VendorSignUp
        public async Task<string> SignUpVendor(SignUpVendorVM model,IFormFile upload)
        {
            bool Status = false;
            string Message = string.Empty;
            //List Of AreaIds
            List<string> ListOfArea = JsonConvert.DeserializeObject<List<string>>(model.AreaIds);
            //List Of CusineIds
            List<string> ListOfCusineIds = JsonConvert.DeserializeObject<List<string>>(model.CuisineIds);

            #region Check User Exist 

            // User Name Already Exsit
            var userName = _userManager.FindByNameAsync(model.Email).Result;
            if (userName != null)
            {
                OtherConstants.messageType = MessageType.Error;
                OtherConstants.isSuccessful = false;
                OtherConstants.responseMsg = "UserName Already Exsit.Please Enter Another UserName";
                return null;
            }

            //User Email Already Exsit
            var userEmail = _userManager.FindByEmailAsync(model.Email).Result;
            if (userEmail != null)
            {
                OtherConstants.messageType = MessageType.Error;
                OtherConstants.isSuccessful = false;
                OtherConstants.responseMsg = "This Email is Already Exsit.Please Enter Another Email";
                return null;
            }
            #endregion

            // User
            var user = new AppUser();
            user.FirstName = model.FirstName;
            user.Created = DateTime.Now;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;

            // Add User In Database 
            var result = _userManager.CreateAsync(user, model.Password).Result;
            if (result.Succeeded)
            {

                string UniqueFileName = "";
                #region Create File Path 

                var datetime = DateTime.Now;
                UniqueFileName = datetime.Month + datetime.Day + datetime.Hour + datetime.Minute + datetime.Ticks + "-" + upload.FileName;
                var path = Directory.GetCurrentDirectory() + "/Uploads/";
                var filemodel = System.IO.Path.Combine(path, UniqueFileName);
                //Store file in Directory Folder 
                using (var stream1 = new FileStream(filemodel, FileMode.Create))
                {
                    upload.CopyToAsync(stream1).Wait();
                }
                #endregion

                //Adding Vendor Information 
                Vendor vendor = new Vendor();

                #region Add other Category 
                Category category = new Category();
                if (model.OtherCatregory != null)
                {
                    //Check If Other Area Already Exist 
                    if (!_listOfAll.CheckAlreadyExistCategory(model.OtherCatregory))
                    {
                        // Save Other Area
                        category.Name = model.OtherCatregory;
                        _efRepository.Add(category);
                        //Adding Cusine With vendor 
                        if (_efRepository.SaveChanges())
                        {
                            vendor.CategoryId = category.Id;
                        }

                    }
                    else
                    {
                        OtherConstants.messageType = MessageType.Error;
                        OtherConstants.isSuccessful = false;
                        OtherConstants.responseMsg = "Other Category Already Exist Please Use an Other One ";
                        //Delete User
                        var userdelete = _userManager.DeleteAsync(user).Result;
                        if (userdelete.Succeeded)
                        {
                            return null;
                        }
                        
                    }
                }
                #endregion

                if (model.OtherCatregory == null)
                {
                    vendor.CategoryId = model.CategoryId;
                }
                vendor.NumberOfLocation = model.NunberOfLocationName;
                vendor.StoreName = model.StoreName;
                vendor.Website_Url = model.Website_Url;
                vendor.UniqueFileName = UniqueFileName;
                vendor.UserId = user.Id;
                vendor.Address_Location = model.Address;
                _efRepository.Add(vendor);
                _efRepository.SaveChanges();

                #region Adding Other Area 

                Area area = null;
                if (model.OtherArea != null)
                {
                    area = new Area();
                    //Check If Other Area Already Exist 
                    if (!_listOfAll.CheckAlreadyExistArea(model.OtherArea))
                    {
                        // Save Other Area
                        area.AreaName = model.OtherArea;
                        _efRepository.Add(area);
                        VendorWithArea driverWithArea = null;
                        if (_efRepository.SaveChanges())
                        {
                            driverWithArea = new VendorWithArea();
                            driverWithArea.AreaId = area.Id;
                            driverWithArea.VendorId = vendor.Id;
                            _efRepository.Add(driverWithArea);
                            var resul = _efRepository.SaveChanges();
                        }

                    }
                    else
                    {
                        //Delete User
                        var userDelete = _userManager.DeleteAsync(user).Result;
                        if (userDelete.Succeeded)
                        {
                            //Delete Driver 
                            _efRepository.Delete(vendor);

                            _efRepository.SaveChanges();
                        }
                        OtherConstants.messageType = MessageType.Error;
                        OtherConstants.isSuccessful = false;
                        OtherConstants.responseMsg = "Other Area Already Exist Please Use an Other One";
                        return null;
                    }
                }

                #endregion

                #region Adding Other Cusine 
                Cuisine cuisine = new Cuisine();
                if (model.OtherCusine != null)
                {

                    //Check If Other Area Already Exist 
                    if (!_listOfAll.CheckAlreadyExistCusine(model.OtherCusine))
                    {
                        // Save Other Area
                        cuisine.Name = model.OtherCusine;
                        _efRepository.Add(cuisine);
                        VendorWithCuisine vendorwithCusine = null;

                        //Adding Cusine With vendor 
                        if (_efRepository.SaveChanges())
                        {
                            vendorwithCusine = new VendorWithCuisine();
                            vendorwithCusine.CuisineId = cuisine.Id;
                            vendorwithCusine.VendorId = vendor.Id;
                            _efRepository.Add(vendorwithCusine);
                            var resul = _efRepository.SaveChanges();
                        }

                    }
                    else
                    {
                        Status = false;
                        Message = "Other Cusine Already Exist Please Use an Other One ";

                        //Delete User
                        var userdelete = _userManager.DeleteAsync(user).Result;
                        if (userdelete.Succeeded)
                        {
                            //Delete Area
                            if (area != null)
                            {
                                _efRepository.Delete(area);
                                //Delete Vendor With Area 
                                var vendorarea = _listOfAll.GetVendorWithAreaById(area.Id);
                                _efRepository.Delete(vendorarea);
                            }

                            //Delete Driver 
                            _efRepository.Delete(vendor);

                            _efRepository.SaveChanges();
                        }

                        OtherConstants.messageType = MessageType.Error;
                        OtherConstants.isSuccessful = Status;
                        OtherConstants.responseMsg = Message;
                        return null;
                    }
                }
                #endregion

                #region Adding List Of Area and Cusine 
                // Save Multipale Area
                if (ListOfArea.Count() > 0)
                {
                    foreach (var item in ListOfArea)
                    {
                        VendorWithArea driverWithArea = new VendorWithArea();
                        driverWithArea.AreaId = Convert.ToInt32(item);
                        driverWithArea.VendorId = vendor.Id;
                        _efRepository.Add(driverWithArea);
                        var resul = _efRepository.SaveChanges();
                    }
                }

                // Save Multipale Cusine 
                if (ListOfCusineIds.Count() > 0)
                {
                    foreach (var item in ListOfCusineIds)
                    {
                        VendorWithCuisine cusineWithArea = new VendorWithCuisine();
                        cusineWithArea.CuisineId = Convert.ToInt32(item);
                        cusineWithArea.VendorId = vendor.Id;
                        _efRepository.Add(cusineWithArea);
                        var resul = _efRepository.SaveChanges();
                    }
                }

                #endregion

                //Add Role
                AddRole("Vendor", user);

                //Send Email Confirmation
                //SendEmailConformationLink(user);

                Status = true;
                Message = "Successfully Created Your Account";

                // Redirect To SignInPage
                OtherConstants.messageType = MessageType.Success;
                OtherConstants.isSuccessful = Status;
                OtherConstants.responseMsg = Message;
                return null;

            }
            else
            {
                Status = false;
                Message = "Error While Creating Your Account";
                OtherConstants.messageType = MessageType.Error;
                OtherConstants.isSuccessful = Status;
                OtherConstants.responseMsg = Message;
                return null;
            }


        }
        #endregion

        #region Add Role 
        public void AddRole(string RoleName, AppUser user)
        {
            string Role = string.Empty;
            // Adding Role If Not Exist
            if (!_roleManager.RoleExistsAsync(RoleName).Result)
            {
                // Add Admin Role If Not Exist
                var adminRole = new AppRole();
                adminRole.Name = RoleName;
                adminRole.Created = DateTime.Now;
                adminRole.Discription = "Perform All Opration";
                // Add Role In Database 
                var addRole = _roleManager.CreateAsync(adminRole).Result;
                if (!addRole.Succeeded)
                {
                    Role = RoleName;
                }
                var role = _userManager.AddToRoleAsync(user, RoleName).Result;

            }
            else
            {
                // Add to Role While Creting User
                var role = _userManager.AddToRoleAsync(user, RoleName).Result;
            }
        }
        #endregion

        #region SignUpDriver
        public async Task<string> SignUpDriver(SignUpDriverVM model)
        {
            bool Status = false;
            string Message = string.Empty;

            //List Of AreaIds
            List<string> ListOfArea = JsonConvert.DeserializeObject<List<string>>(model.AreaIds);

            // User Name Already Exsit
            var userName = _userManager.FindByNameAsync(model.Email).Result;
            if (userName != null)
            {
                Message = "UserName Already Exsit.Please Enter Another UserName";
                Status = false;
                OtherConstants.messageType = MessageType.Error;
                OtherConstants.isSuccessful = Status;
                OtherConstants.responseMsg = Message;
                return null;
            }

            //User Email Already Exsit
            var userEmail = _userManager.FindByEmailAsync(model.Email).Result;
            if (userEmail != null)
            {
                Message = "This Email is Already Exsit.Please Enter Another Email";
                Status = false;
                OtherConstants.messageType = MessageType.Error;
                OtherConstants.isSuccessful = Status;
                OtherConstants.responseMsg = Message;
                return null;
            }

            // User
            var user = new AppUser();
            user.FirstName = model.FirstName;
            user.Created = DateTime.Now;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;


            // Add User In Database 
            var result = _userManager.CreateAsync(user, model.Password).Result;

            if (result.Succeeded)
            {

                // Save Driver Information
                Driver driver = new Driver();
                driver.Address_Location = model.Address;
                driver.UserId = user.Id;
                _efRepository.Add(driver);
                _efRepository.SaveChanges();

                Area area = new Area();
                if (model.OtherArea != null)
                {

                    //Check If Other Area Already Exist 
                    if (!_listOfAll.CheckAlreadyExistArea(model.OtherArea))
                    {
                        // Save Other Area
                        area.AreaName = model.OtherArea;
                        _efRepository.Add(area);
                        if (_efRepository.SaveChanges())
                        {
                            DriverWithArea driverWithArea = new DriverWithArea();
                            driverWithArea.AreaId = area.Id;
                            driverWithArea.DriverId = driver.Id;
                            _efRepository.Add(driverWithArea);
                            var resul = _efRepository.SaveChanges();
                        }

                    }
                    else
                    {
                        Status = false;
                        Message = "Other Area Already Exist Please Use an Other One ";

                        //Delete User
                        var date = _userManager.DeleteAsync(user).Result;
                        if (date.Succeeded)
                        {
                            //Delete Driver 
                            _efRepository.Delete(driver);
                            _efRepository.SaveChanges();
                        }
                        OtherConstants.messageType = MessageType.Error;
                        OtherConstants.isSuccessful = Status;
                        OtherConstants.responseMsg = Message;
                        return null;
                    }
                }

                // Save Multipale Area
                if (ListOfArea.Count() > 0)
                {
                    foreach (var item in ListOfArea)
                    {
                        DriverWithArea driverWithArea = new DriverWithArea();
                        driverWithArea.AreaId = Convert.ToInt32(item);
                        driverWithArea.DriverId = driver.Id;
                        _efRepository.Add(driverWithArea);
                        var resul = _efRepository.SaveChanges();
                    }
                }

                //Add Role
                AddRole("Driver", user);

                //Send Email Confirmation
                //SendEmailConformationLink(user);

                Status = true;
                Message = "Successfully Created Your Account";
                OtherConstants.messageType = MessageType.Success;
                OtherConstants.isSuccessful = Status;
                OtherConstants.responseMsg = Message;
                return null;
            }
            else
            {
                Status = false;
                Message = "Error While Creating Your Account";
                OtherConstants.messageType = MessageType.Error;
                OtherConstants.isSuccessful = Status;
                OtherConstants.responseMsg = Message;
                return null;
            }

        }
        #endregion

        #region SignUp User
        public async Task<string> SignUpUser(SignUpUserVM model)
        {
            bool Status = false;
            string Message = string.Empty;

            // User Name Already Exsit
            var userName = _userManager.FindByNameAsync(model.Email).Result;
            if (userName != null)
            {
                Message = "UserName Already Exsit.Please Enter Another UserName";
                Status = false;
                OtherConstants.messageType = MessageType.Error;
                OtherConstants.isSuccessful = Status;
                OtherConstants.responseMsg = Message;
                return null;
            }

            //User Email Already Exsit
            var userEmail = _userManager.FindByEmailAsync(model.Email).Result;
            if (userEmail != null)
            {
                Message = "This Email is Already Exsit.Please Enter Another Email";
                Status = false;
                OtherConstants.messageType = MessageType.Error;
                OtherConstants.isSuccessful = Status;
                OtherConstants.responseMsg = Message;
                return null;
            }

            // User
            var user = new AppUser();
            user.FirstName = model.FirstName;
            user.Created = DateTime.Now;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            // Add User In Database 
            var result = _userManager.CreateAsync(user, model.Password).Result;
            if (result.Succeeded)
            {
                //Add Role
                AddRole("User", user);
                //Send Email Confirmation
                //SendEmailConformationLink(user);
                Status = true;
                Message = "Successfully Created Your Account";
                OtherConstants.messageType = MessageType.Success;
                OtherConstants.isSuccessful = Status;
                OtherConstants.responseMsg = Message;
                return null;

            }
            else
            {
                Status = false;
                Message = "Error While Creating Your Account";
                OtherConstants.messageType = MessageType.Error;
                OtherConstants.isSuccessful = Status;
                OtherConstants.responseMsg = Message;
                return null;
            }
        }
        #endregion

        #region Delete Driver User and Vendor
        // POST: Vendor/Delete/5
        public async Task<string> DeleteVendor(string id)
        {
            bool Status = false;
            string Message = string.Empty;


            // Finde vendor 
            var Vendor = _listOfAll.GetVendorByUserId(id);

            //Vendor With Area
            var VendorWithArea = _listOfAll.GetsVendorWithAreaById(Vendor.Id);
            foreach (var item in VendorWithArea)
            {
                //Delete Driver 
                _efRepository.Delete(item);
            }

            //Vendor With Cusine
            var VendorWithCusine = _listOfAll.GetsVendorWithCusineById(Vendor.Id);
            foreach (var item in VendorWithCusine)
            {
                //Delete Driver 
                _efRepository.Delete(item);
            }

            //Check Vendor Other Location
            var VendorOtherLocation = _listOfAll.GetOtherLocationByVendorID(Vendor.Id);
            foreach (var item in VendorOtherLocation)
            {
                //Delete Other Location 
                _efRepository.Delete(item);
            }


            //Delete Driver 
            _efRepository.Delete(Vendor);

            _efRepository.SaveChanges();

            var user = _userManager.FindByIdAsync(id).Result;
            var result = _userManager.DeleteAsync(user).Result;
            if (result.Succeeded)
            {
              
                Message = "Record Successfully Deleted.";
                OtherConstants.messageType = MessageType.Success;
                OtherConstants.isSuccessful = true;
                OtherConstants.responseMsg = Message;
                return null;

            }
            else
            {
                Status = false;
                Message = "Error While Deleted Record.";
                OtherConstants.messageType = MessageType.Error;
                OtherConstants.isSuccessful = Status;
                OtherConstants.responseMsg = Message;
                return null;
            }
        }


        // POST: Vendor/Delete/5
        public async Task<string> DeleteDriver(string id)
        {
            bool Status = false;
            string Message = string.Empty;


            // Finde vendor 
            var Driver = _listOfAll.GetDriverByUserId(id);

            //Vendor With Area
            var DriverWithArea = _listOfAll.GetsDriverWithAreaById(Driver.Id);
            foreach (var item in DriverWithArea)
            {
                //Delete Driver 
                _efRepository.Delete(item);
            }

            //Delete Driver 
            _efRepository.Delete(Driver);

            _efRepository.SaveChanges();

            var user = _userManager.FindByIdAsync(id).Result;
            var result = _userManager.DeleteAsync(user).Result;
            if (result.Succeeded)
            {
                Status = true;
                Message = "Record Successfully Deleted.";
                OtherConstants.messageType = MessageType.Success;
                OtherConstants.isSuccessful = Status;
                OtherConstants.responseMsg = Message;
                return null;

            }
            else
            {
                Status = false;
                Message = "Error While Deleted Record.";
                OtherConstants.messageType = MessageType.Error;
                OtherConstants.isSuccessful = Status;
                OtherConstants.responseMsg = Message;
                return null;
            }
        }


        // POST: User/Delete/5
        public async Task<string> DeleteUser(string id)
        {
            bool Status = false;
            string Message = string.Empty;
            var user = _userManager.FindByIdAsync(id).Result;
            var result = _userManager.DeleteAsync(user).Result;
            if (result.Succeeded)
            {
                Status = true;
                Message = "Record Successfully Deleted.";
                OtherConstants.messageType = MessageType.Success;
                OtherConstants.isSuccessful = Status;
                OtherConstants.responseMsg = Message;
                return null;

            }
            else
            {
                Status = false;
                Message = "Error While Deleted Record.";
                OtherConstants.messageType = MessageType.Error;
                OtherConstants.isSuccessful = Status;
                OtherConstants.responseMsg = Message;
                return null;
            }
          
        }
        #endregion

        #region Get All Driver,Vendor,User
        public async Task<IEnumerable<SignUpUserVM>> AllUser()
        {

            var data = _userManager.Users.ToList();
            List<SignUpUserVM> model = new List<SignUpUserVM>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    if (_userManager.IsInRoleAsync(item, "User").Result)
                    {
                        SignUpUserVM listVM = new SignUpUserVM();
                        listVM.Id = item.Id;
                        listVM.FirstName = item.FirstName;
                        listVM.LastName = item.LastName;
                        listVM.Email = item.Email;
                        listVM.PhoneNumber = item.PhoneNumber;
                        model.Add(listVM);
                    }

                }
            }

            return model;
        }



        public async Task<IEnumerable<SignUpDriverVM>> AllDriver()
        {

            var data = _userManager.Users.ToList();
            List<SignUpDriverVM> model = new List<SignUpDriverVM>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    if (_userManager.IsInRoleAsync(item, "Driver").Result)
                    {
                        var driverData = _listOfAll.GetDriverByUserId(item.Id);
                        if (driverData != null)
                        {
                            SignUpDriverVM listVM = new SignUpDriverVM();
                            listVM.Id = item.Id;
                            listVM.FirstName = item.FirstName;
                            listVM.LastName = item.LastName;
                            listVM.Email = item.Email;
                            listVM.PhoneNumber = item.PhoneNumber;
                            listVM.Address = driverData.Address_Location;
                            model.Add(listVM);
                        }
                    }
                }
            }
            return model;
        }


        public async Task<IEnumerable<SignUpVendorVM>> AllVendor()
        {

            var data = _userManager.Users.ToList();
            List<SignUpVendorVM> model = new List<SignUpVendorVM>();
            if (data != null)
            {

                foreach (var item in data)
                {
                    if (_userManager.IsInRoleAsync(item, "Vendor").Result)
                    {

                        var VendorData = _listOfAll.GetVendorByUserId(item.Id);
                        if (VendorData != null)
                        {
                            SignUpVendorVM listVM = new SignUpVendorVM();
                            listVM.Id = item.Id;
                            listVM.FirstName = item.FirstName;
                            listVM.LastName = item.LastName;
                            listVM.Email = item.Email;
                            listVM.PhoneNumber = item.PhoneNumber;
                            listVM.StoreName = VendorData.StoreName;
                            listVM.Address = VendorData.Address_Location;
                            listVM.VendorId = VendorData.Id;
                            model.Add(listVM);
                        }

                    }
                }
            }
            return model;
        }
        #endregion

        public async Task<UserLoginWithTokenVM> ProcessLogin(LoginViewModel model)
        {
            UserLoginWithTokenVM customer = new UserLoginWithTokenVM();
            var user = await _userManager.FindByEmailAsync(model.email);

            //Check User Exist 
            if (user == null)
            {
                OtherConstants.messageType = MessageType.Error;
                OtherConstants.isSuccessful = false;
                OtherConstants.responseMsg = "Invalid username or password!";
                return null;

            }



            //Check Email Confirm
            if (!user.EmailConfirmed)
            {
                OtherConstants.responseMsg = "User is not approved yet. Please contact system administration";
                OtherConstants.isSuccessful = false;
                return null;
            }


            var userRole = await _userManager.GetRolesAsync(user);
            var signIn = await _signInManager.PasswordSignInAsync(user, model.password, true, true);


            if (signIn.Succeeded)
            {
                string roles = JsonConvert.SerializeObject(userRole);
                List<Claim> claims = new List<Claim>()
                    {
                        new Claim("UserId", user.Id),
                        new Claim("UserName",user.UserName),
                        new Claim("Roles",roles)
                    };
                var identity = new ClaimsIdentity(claims, "Basic");
                var claimsPrincipal = new ClaimsPrincipal(identity);

                // Set current principal
                Thread.CurrentPrincipal = claimsPrincipal;

                //Token
                var token = TokenBuilder.CreateJsonWebToken(user.Email, new List<string>()
                    { "Administrator" }, string.Empty, string.Empty, Guid.NewGuid(),
                DateTime.UtcNow.AddMinutes(60), claims);

                // customer = mapper.Map<CustomersVM>(user);
                customer.accessToken = new JwtSecurityTokenHandler().WriteToken(token);
                customer.tokenExpiry = token.ValidTo;
                OtherConstants.isSuccessful = true;
                customer.id = user.Id;
                foreach (var item in userRole)
                {
                    customer.role = new RoleVM();
                    var role = _roleManager.FindByNameAsync(item).Result;
                    customer.role.Name = role.Name;
                    customer.role.Id = role.Id;
                }
                customer.liscences = " "; //user.license.Title;
            }
            else
            {
                OtherConstants.messageType = MessageType.Error;
                OtherConstants.isSuccessful = false;
                OtherConstants.responseMsg = "Invalid username or password!";
                return null;
            }

            return customer;
        }
        public IEnumerable<RoleVM> GetRoles()
        {
            var roles = _roleManager.Roles.Where(x => x.Name != "Super Admin").ToList();
            List<RoleVM> lstvm = new List<RoleVM>();
            foreach (var r in roles)
            {
                var obj = new RoleVM();
                lstvm.Add(obj);
            }
            return lstvm;

        }
        // Here Is Send Email Methods with SMTP
        public string sendMail(string toEmail, string emailSubject, string emailBody, string fromEmailAddress)
        {
            bool Status = false;
            string Message = string.Empty;
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                mail.To.Add(toEmail);
                mail.From = new MailAddress(fromEmailAddress);
                mail.Subject = emailSubject;
                //Body of the email
                mail.Body = emailBody;

                //Get client credentails from web.config
                client.Credentials = new System.Net.NetworkCredential
                ("grubNow12@gmail.com", "Test@0000");
                client.Send(mail);
                Message = "Email Send Successfully";
                Status = true;
                OtherConstants.messageType = MessageType.Success;
            }
            catch (Exception ex)
            {
                Message = "Error " + ex.Message;
                Status = false;
                OtherConstants.messageType = MessageType.Error;
            }

 
            return Message;
            
        }
        public string ConfirmEmail(string Id, string token)
        {
            var UserWithGuid = _context.Users.FirstOrDefault(x => x.Id == Id);
            if (UserWithGuid != null)
            {
                var user = _userManager.FindByIdAsync(UserWithGuid.Id).Result;
                if (user != null)
                {
                    var result = _userManager.ConfirmEmailAsync(user, token).Result;
                    if (result.Succeeded)
                    {
                        OtherConstants.responseMsg = "Successfully Approved Your Account";
                        OtherConstants.isSuccessful = true;
                    }
                    else
                    {
                        OtherConstants.responseMsg = "Your Account Is Not Approved Please Contact Addministrator";
                        OtherConstants.isSuccessful = false;
                    }
                }
            }
            return "ok";
        }

        #region List Of Resturant By Area
        public async Task<IEnumerable<ListOfResturantVM>> ListResturentByArea(string Area)
        {
            var data = _listOfAll.GetAreaByName(Area);
            List<ListOfResturantVM> listOfs = new List<ListOfResturantVM>();
            if (data != null)
            {
                foreach (var item in data.VendorWithAreas)
                {
                    var vendor = _listOfAll.GetVendorById(item.VendorId ?? 0);
                    if (vendor.Category.Name == "Restaurants")
                    {
                        ListOfResturantVM resturantVM = new ListOfResturantVM();
                        resturantVM.Category = vendor.Category.Name;
                        resturantVM.Id = vendor.Id;
                        resturantVM.StoreName = vendor.StoreName;
                        resturantVM.UniqueFileName = vendor.UniqueFileName;
                        resturantVM.Website_Url = vendor.Website_Url;
                        resturantVM.Address_Location = vendor.Address_Location;
                        listOfs.Add(resturantVM);
                    }
                }
            }

            return listOfs;
        }
        #endregion

        #region List Of All Resturent
        public async Task<IEnumerable<ListOfResturantVM>> ListResturent()
        {
            List<ListOfResturantVM> listOfs = new List<ListOfResturantVM>();

            var vendor = _listOfAll.GetVendor();
            if (vendor != null)
            {
                foreach (var item in vendor)
                {
                    if (item.Category.Name == "Restaurants")
                    {
                        ListOfResturantVM resturantVM = new ListOfResturantVM();
                        resturantVM.Category = item.Category.Name;
                        resturantVM.Id = item.Id;
                        resturantVM.StoreName = item.StoreName;
                        resturantVM.UniqueFileName = item.UniqueFileName;
                        resturantVM.Website_Url = item.Website_Url;
                        resturantVM.Address_Location = item.Address_Location;
                        listOfs.Add(resturantVM);
                    }
                }
            }
            return listOfs;
        }
        #endregion

        #region Edit Vendor
        /// <summary>
        /// Edit Vendor Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EditVendorVM EditVendor(string id)
        {
            EditVendorVM vendorVM = null;
            var user = _userManager.FindByIdAsync(id).Result;
            if (user != null)
            {

                vendorVM = new EditVendorVM();
                vendorVM.Id = user.Id;
                vendorVM.FirstName = user.FirstName;
                vendorVM.LastName = user.LastName;
                vendorVM.PhoneNumber = user.PhoneNumber;
                vendorVM.Email = user.Email;

                // Geting Vendor Data
                var vendor = _listOfAll.GetVendorByUserId(id);
                if (vendor != null)
                {
                    vendorVM.CategoryId = vendor.Category.Id;
                    vendorVM.StoreName = vendor.StoreName;
                    vendorVM.Website_Url = vendor.Website_Url;
                    vendorVM.Address = vendor.Address_Location;
                    vendorVM.NunberOfLocationName = vendor.NumberOfLocation;
                }
            }
            return vendorVM;
        }



        /// <summary>
        /// Edit Post Methods
        /// </summary>
        /// <param name="model"></param>
        /// <param name="upload"></param>
        /// <returns></returns>
        public async Task<string> EditVendor(EditVendorVM model, IFormFile upload)
        {
            string Message = string.Empty;
            bool Status = false;
            var userVernder = _userManager.FindByIdAsync(model.Id).Result;
            if (userVernder != null)
            {
                userVernder.Id = model.Id;
                userVernder.FirstName = model.FirstName;
                userVernder.LastName = model.LastName;
                userVernder.PhoneNumber = model.PhoneNumber;
                userVernder.Email = model.Email;
                var updateuser = _userManager.UpdateAsync(userVernder).Result;
                if (updateuser.Succeeded)
                {

                    // Geting Vendor Data
                    var vendor = _listOfAll.GetVendorByUserId(model.Id);
                    if (vendor != null)
                    {
                        string UniqueFileName = "";
                        try
                        {
                            #region Create File Path 

                            var datetime = DateTime.Now;
                            UniqueFileName = datetime.Month + datetime.Day + datetime.Hour + datetime.Minute + datetime.Ticks + "-" + upload.FileName;
                            var path = Directory.GetCurrentDirectory() + "/Uploads/";
                            var filemodel = System.IO.Path.Combine(path, UniqueFileName);
                            //Store file in Directory Folder 
                            using (var stream1 = new FileStream(filemodel, FileMode.Create))
                            {
                                upload.CopyToAsync(stream1).Wait();
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            OtherConstants.messageType = MessageType.Error;
                            OtherConstants.isSuccessful = false;
                            OtherConstants.responseMsg = ex.Message;
                            return null;

                        }
                        vendor.CategoryId = model.CategoryId;
                        vendor.StoreName = model.StoreName;
                        vendor.Website_Url = model.Website_Url;
                        vendor.UniqueFileName = UniqueFileName;
                        vendor.Address_Location = model.Address;
                        _efRepository.Update(vendor);
                        if (_efRepository.SaveChanges())
                        {
                            Message = " Successfully Update Record";
                            Status = true;
                            OtherConstants.messageType = MessageType.Success;
                            OtherConstants.isSuccessful = Status;
                            OtherConstants.responseMsg = Message;
                            return null;
                        }
                        else
                        {
                            Message = " Error While Updating Record";
                            Status = true;
                            OtherConstants.messageType = MessageType.Error;
                            OtherConstants.isSuccessful = Status;
                            OtherConstants.responseMsg = Message;
                            return null;
                        }
                    }
                }

            }
            else
            {
                Message = "User Con Not Find";
                Status = false;
            }


            OtherConstants.messageType = MessageType.Error;
            OtherConstants.isSuccessful = Status;
            OtherConstants.responseMsg = Message;
            return null;
        }
        #endregion

        #region Edit Driver
        public EditDriverVM EditDriver(string id)
        {
            EditDriverVM model = null;
            var user = _userManager.FindByIdAsync(id).Result;
            if (user != null)
            {
                model = new EditDriverVM();
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                model.PhoneNumber = user.PhoneNumber;
                model.Id = user.Id;

                var driver = _listOfAll.GetDriverByUserId(id);
                if (driver != null)
                {
                    model.Address = driver.Address_Location;
                }
                // model.AreaId =driver.
            }
            return model;
        }

        /// <summary>
        /// Edit Driver Post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseResponse EditDriver(EditDriverVM model)
        {
            string Message = string.Empty;
            bool Status = false;

            var userDriver = _userManager.FindByIdAsync(model.Id).Result;
            if (userDriver != null)
            {
                userDriver.Id = model.Id;
                userDriver.FirstName = model.FirstName;
                userDriver.LastName = model.LastName;
                userDriver.PhoneNumber = model.PhoneNumber;
                userDriver.Email = model.Email;
                var user = _userManager.UpdateAsync(userDriver).Result;
                if (user.Succeeded)
                {
                    var driver = _listOfAll.GetDriverByUserId(model.Id);
                    if (driver != null)
                    {
                        driver.Address_Location = model.Address;
                        _efRepository.Update(driver);
                        if (_efRepository.SaveChanges())
                        {
                            Message = " Successfully Update Record";
                            Status = true;
                            OtherConstants.messageType = MessageType.Success;
                            OtherConstants.isSuccessful = Status;
                            OtherConstants.responseMsg = Message;
                            return null;
                        }
                        else
                        {
                            Message = " Error While Updating Record";
                            Status = false;
                            OtherConstants.messageType = MessageType.Error;
                            OtherConstants.isSuccessful = Status;
                            OtherConstants.responseMsg = Message;
                            return null;

                        }
                    }
                }

            }
            else
            {
                Message = "Driver Not Exist";
                Status = false;
            }
            OtherConstants.messageType = MessageType.Error;
            OtherConstants.isSuccessful = Status;
            OtherConstants.responseMsg = Message;
            return null;
        }
        #endregion

        #region Edit User
        public EditUserVM EditUser(string id)
        {
            EditUserVM model = null;
            var user = _userManager.FindByIdAsync(id).Result;
            if (user != null)
            {
                model = new EditUserVM();
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                model.PhoneNumber = user.PhoneNumber;
                model.Id = user.Id;
            }
            return model;
        }

        public async Task<string> EditUser(EditUserVM model)
        {

            string Message = string.Empty;
            bool Status = false;
            var userDriver = _userManager.FindByIdAsync(model.Id).Result;
            if (userDriver != null)
            {
                userDriver.Id = model.Id;
                userDriver.FirstName = model.FirstName;
                userDriver.LastName = model.LastName;
                userDriver.PhoneNumber = model.PhoneNumber;
                userDriver.Email = model.Email;
                var user = _userManager.UpdateAsync(userDriver).Result;
                if (user.Succeeded)
                {
                    Message = " Successfully Update Record";
                    Status = true;
                    OtherConstants.messageType = MessageType.Success;
                    OtherConstants.isSuccessful = Status;
                    OtherConstants.responseMsg = Message;
                    return null;
                }
                else
                {
                    Message = " Error While Updating Record";
                    Status = false;
                }

            }
            OtherConstants.messageType = MessageType.Error;
            OtherConstants.isSuccessful = Status;
            OtherConstants.responseMsg = Message;
            return null;
        }
        #endregion

        #region Add Other Location
        public async Task<string> Addlocation(OtherLocationVM Model)
        {
            string Message = string.Empty;
            bool Status = false;

            OtherLocation obj = new OtherLocation();
            obj.VendorID = Model.VendorID;
            obj.LocationAddress = Model.LocationAddress;
            obj.LocationName = Model.LocationName;
            _efRepository.Add(obj);
            var result = _efRepository.SaveChanges();
            if (result)
            {
                Status = true;
                Message = "Record Successfully Created.";
                OtherConstants.messageType = MessageType.Success;
                OtherConstants.isSuccessful = Status;
                OtherConstants.responseMsg = Message;
                return null;
            }
            else
            {
                Status = false;
                Message = "Error While Creating Record.";
                OtherConstants.messageType = MessageType.Error;
                OtherConstants.isSuccessful = Status;
                OtherConstants.responseMsg = Message;
                return null;
            }
        }
        #endregion

        #region List Other Location
        public IEnumerable<OtherLocationList> ListOfLocation(int VendorId)
        {
            List<OtherLocationList> otherLocaction = new List<OtherLocationList>();
            var getOtherLocationByVendorID = _listOfAll.GetOtherLocationByVendorID(VendorId).ToList();
            foreach (var item in getOtherLocationByVendorID)
            {
                otherLocaction.Add(new OtherLocationList()
                {
                    LocationAddress = item.LocationAddress,
                    LocationName = item.LocationName
                });
            }
            return otherLocaction;
        }
        #endregion


        #region UserProfile

        public UserProfileVM UserProfile(string UserId)
        {
            UserProfileVM userProfileVM = new UserProfileVM();
            if (UserId != null)
            {
                var result = _userManager.FindByIdAsync(UserId).Result;
                if (result != null)
                {
                    userProfileVM.CarPic = result.DriverCar;
                    userProfileVM.Email = result.Email;
                    userProfileVM.FirstName = result.FirstName;
                    userProfileVM.LastName = result.LastName;
                    userProfileVM.PhoneNumber = result.PhoneNumber;
                    userProfileVM.ProfilePic = result.ProfilePic;
                }
            }
            return userProfileVM;
        }


        public async Task<string> UpdateProfile(UserProfilesVM model)
        {
            bool Status = false;
            string Message = string.Empty;
            var user = _userManager.FindByIdAsync(model.Id).Result;
            if (user != null)
            {
                string fileName = string.Empty;
                string CarfileName = string.Empty;
                if (model.ProfilePic!=null)
                {
                    string rootPath = Directory.GetCurrentDirectory() + "/Uploads";
                    fileName = System.IO.Path.Combine(rootPath, model.ProfilePic.FileName);
                    //Store file in Directory Folder 
                    using (var stream = new FileStream(fileName, FileMode.Create))
                        model.ProfilePic.CopyTo(stream);
                }

                if (model.CarPic != null)
                {
                    string rootPath = Directory.GetCurrentDirectory() + "/Uploads";
                    CarfileName = System.IO.Path.Combine(rootPath, model.CarPic.FileName);
                    //Store file in Directory Folder 
                    using (var stream = new FileStream(CarfileName, FileMode.Create))
                        model.ProfilePic.CopyTo(stream);
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.ProfilePic = fileName;
                user.DriverCar = CarfileName;
                var result1 = _userManager.UpdateAsync(user).Result;
                if (result1.Succeeded)
                {
                    Status = true;
                    Message = "Record Update successfully";
                    OtherConstants.messageType = MessageType.Success;
                    OtherConstants.isSuccessful = Status;
                    OtherConstants.responseMsg = Message;
                    return null;
                }
                else
                {
                    Status = false;
                    Message = "Error While Updating record";
                    OtherConstants.messageType = MessageType.Success;
                    OtherConstants.isSuccessful = Status;
                    OtherConstants.responseMsg = Message;
                    return null;
                }
            }
            return null;
        }


        #endregion
    }
}
