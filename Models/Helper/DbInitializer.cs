//using EntityLayer.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;

//namespace EntityLayer.Helper
//{
//    public static class DbInitializer
//    {
//        private static PlenumDbContext ctx;
//        public static void Initialize(PlenumDbContext ctx, UserManager<ExtendedUser> userManager, RoleManager<ExtendedRole> roleManager)
//        {
//            DbInitializer.ctx = ctx;

//            if (!ctx.Faculty.Any() && !ctx.Course.Any() && !ctx.Programs.Any())
//                SeedFacultyCourseProgram();

//            if (!ctx.Roles.Any())
//                SeedRoles(roleManager);

//            if (!ctx.Users.Any())
//                SeedUsers(userManager);

//            if (!ctx.GradingPoints.Any())
//                SeedGradingPoints();

//            if (!ctx.DegreeClass.Any())
//                SeedDegreeClass();

//            if (!ctx.Examinations.Any())
//                SeedPOP();

//            if (!ctx.States.Any() && !ctx.Cities.Any())
//                SeedStatesAndCities();

//            if (!ctx.ProgramCompulsoryFee.Any())
//                SeedProgramCompulsoryFee();

//            if (!ctx.Bookmarks.Any())
//                SeedBookmarks();

//            if (!ctx.SecurityQuestions.Any())
//                SeedSecurityQuestions();

//            if (!ctx.License.Any())
//                SeedLicenses();
//        }

//        private static void SeedRoles(RoleManager<ExtendedRole> roleManager)
//        {
//            //Collection Of Roles
//            List<ExtendedRole> roles = new List<ExtendedRole>();

//            roles.Add(new ExtendedRole() { Id = "078cacd7-a7ef-4609-b49a-a970aa61f729", Name = "Admin", canLogin = true });
//            roles.Add(new ExtendedRole() { Id = "879231a5-90ed-485d-9355-8811513bb4ce", Name = "Agent", ParentRoleId = "078cacd7-a7ef-4609-b49a-a970aa61f729", canLogin = true });
//            roles.Add(new ExtendedRole() { Id = "aa00c8ce-124f-4323-b4a3-8cc905c7394f", Name = "Student", ParentRoleId = "078cacd7-a7ef-4609-b49a-a970aa61f729", canLogin = true });
//            // Add Ranges of Role to Database 
//            foreach (var user in roles)
//            {
//                roleManager.CreateAsync(user).Wait();
//            }
//        }
//        private static void SeedUsers(UserManager<ExtendedUser> userManager)
//        {
//            var MyPassword = "Test@0000";
//            List<ExtendedUser> users = new List<ExtendedUser>();
//            users.Add(new ExtendedUser() { Id = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", UserName = "Admin@plenum.com", Email = "Admin@plenum.com", EmailConfirmed = true, isApproved = true, hasActiveLicense = true });

//            //sample agent
//            ExtendedUser u = new ExtendedUser() { Id = "6027d5ea-192d-47af-b961-a80156d4bc29", UserName = "Agent1@plenum.com", Email = "Agent1@plenum.com", EmailConfirmed = true, isApproved = true, hasActiveLicense = true };
//            u.AgentDetails = new AgentDetails() { IdentificationNumber = "394729034234", IdentificationType = "IDType" };
//            u.AgentDetails.AgentServices = new AgentServices() { AdmissionProcessing = "Yes", Assignment = "Yes" };
//            users.Add(u);

//            //sample student
//            u = new ExtendedUser() { Id = "6cb08a7d-f928-4867-846d-00cc140aca52", UserName = "Student1@gmail.com", Email = "Student1@gmail.com", EmailConfirmed = true, isApproved = true, hasActiveLicense = true };
//            u.StudentDetails = new StudentDetails() { ProgramId = 1 };
//            users.Add(u);

//            foreach (var user in users)
//            {
//                userManager.CreateAsync(user, MyPassword).Wait();

//                if (user.AgentDetails != null)
//                    userManager.AddToRoleAsync(user, "Agent").Wait();

//                else if (user.StudentDetails != null)
//                    userManager.AddToRoleAsync(user, "Student").Wait();

//                else
//                    userManager.AddToRoleAsync(user, "Admin").Wait();
//            }
//        }
//        private static void SeedGradingPoints()
//        {
//            List<GradingPoints> GradingPoints = new List<GradingPoints>();

//            GradingPoints.Add(new GradingPoints() { LowerLimit = 0, UpperLimit = 39, Grade = "F", WeightedGradePoint = 0, CreatedDate = DateTime.Now });
//            GradingPoints.Add(new GradingPoints() { LowerLimit = 0, UpperLimit = 39, Grade = "I", WeightedGradePoint = 0, CreatedDate = DateTime.Now });
//            GradingPoints.Add(new GradingPoints() { LowerLimit = 40, UpperLimit = 44, Grade = "E", WeightedGradePoint = 1, CreatedDate = DateTime.Now });
//            GradingPoints.Add(new GradingPoints() { LowerLimit = 45, UpperLimit = 49, Grade = "D", WeightedGradePoint = 2, CreatedDate = DateTime.Now });
//            GradingPoints.Add(new GradingPoints() { LowerLimit = 50, UpperLimit = 59, Grade = "C", WeightedGradePoint = 3, CreatedDate = DateTime.Now });
//            GradingPoints.Add(new GradingPoints() { LowerLimit = 60, UpperLimit = 69, Grade = "B", WeightedGradePoint = 4, CreatedDate = DateTime.Now });
//            GradingPoints.Add(new GradingPoints() { LowerLimit = 70, UpperLimit = 100, Grade = "A", WeightedGradePoint = 5, CreatedDate = DateTime.Now });

//            foreach (var grPoint in GradingPoints)
//            {
//                ctx.GradingPoints.Add(grPoint);
//                ctx.SaveChanges();
//            }
//        }
//        private static void SeedDegreeClass()
//        {
//            List<DegreeClass> DegreeClasses = new List<DegreeClass>();

//            DegreeClasses.Add(new DegreeClass() { LowerLimit = 0, UpperLimit = 0.99, AwardedClass = "Fail", CreatedDate = DateTime.Now });
//            DegreeClasses.Add(new DegreeClass() { LowerLimit = 1.50, UpperLimit = 2.49, AwardedClass = "Third Class", CreatedDate = DateTime.Now });
//            DegreeClasses.Add(new DegreeClass() { LowerLimit = 2.50, UpperLimit = 3.49, AwardedClass = "Second Class (Lower)", CreatedDate = DateTime.Now });
//            DegreeClasses.Add(new DegreeClass() { LowerLimit = 3.50, UpperLimit = 4.49, AwardedClass = "Second Class (Upper)", CreatedDate = DateTime.Now });
//            DegreeClasses.Add(new DegreeClass() { LowerLimit = 4.5, UpperLimit = 5.00, AwardedClass = "First Class", CreatedDate = DateTime.Now });
//            foreach (var cl in DegreeClasses)
//            {
//                ctx.DegreeClass.Add(cl);
//                ctx.SaveChanges();
//            }
//        }
//        private static void SeedPOP()
//        {
//            List<Examination> DegreeClasses = new List<Examination>();

//            DegreeClasses.Add(new Examination() { Name = "ECO348.pdf", CourseId = 14, ExamFile = "ECO348.pdf", ExamType = "POP", CreatedDate = DateTime.Now });
//            DegreeClasses.Add(new Examination() { Name = "MAC413QUESTIONS2019_2 NEW.pdf", CourseId = 24, ExamFile = "MAC413QUESTIONS2019_2 NEW.pdf", ExamType = "POP", CreatedDate = DateTime.Now });
//            foreach (var cl in DegreeClasses)
//            {
//                ctx.Examinations.Add(cl);
//                ctx.SaveChanges();
//            }
//        }
//        private static void SeedFacultyCourseProgram()
//        {
//            string path = Path.Combine(Directory.GetCurrentDirectory() + "/SqlScript/scriptPlenum.sql");
//            var sql = File.ReadAllText(path);
//            var row = ctx.Database.ExecuteSqlRaw(sql);
//        }
//        private static void SeedStatesAndCities()
//        {
//            List<City> cities1 = new List<City>();
//            cities1.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "47.02907000", Longitude = "18.52172000", Name = "Aba" });
//            cities1.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.78917000", Longitude = "7.83829000", Name = "Amaigbo" });
//            cities1.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.38941000", Longitude = "7.91235000", Name = "Arochukwu" });
//            cities1.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.55874000", Longitude = "7.63359000", Name = "Bende" });
//            cities1.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.61455000", Longitude = "7.81191000", Name = "Ohafia-Ifigh" });
//            cities1.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.52491000", Longitude = "7.49461000", Name = "Umuahia" });
//            State state1 = new State();
//            state1.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state1.Name = "Abia State";
//            state1.CreatedDate = DateTime.Now;
//            ctx.States.Add(state1);
//            ctx.SaveChanges();
//            foreach (var item in cities1)
//            {
//                item.StateId = state1.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities2 = new List<City>();
//            cities2.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "4.80293000", Longitude = "8.25341000", Name = "Esuk Oron" });
//            cities2.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.18194000", Longitude = "7.71481000", Name = "Ikot Ekpene" });
//            cities2.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.20131000", Longitude = "7.98373000", Name = "Itu" });
//            cities2.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.05127000", Longitude = "7.93350000", Name = "Uyo" });
//            State state2 = new State();
//            state2.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state2.Name = "Akwa Ibom State";
//            state2.CreatedDate = DateTime.Now;
//            ctx.States.Add(state2);
//            ctx.SaveChanges();
//            foreach (var item in cities2)
//            {
//                item.StateId = state2.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            //TODO
//            List<City> cities3 = new List<City>();
//            cities3.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "40.50417000", Longitude = "20.22722000", Name = "Çorovodë" });
//            cities3.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "40.82492000", Longitude = "19.84074000", Name = "Banaj" });
//            cities3.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "40.69997000", Longitude = "19.94983000", Name = "Bashkia Berat" });
//            cities3.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "40.82489000", Longitude = "19.95350000", Name = "Bashkia Kuçovë" });
//            cities3.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "40.58608000", Longitude = "20.04535000", Name = "Bashkia Poliçan" });
//            cities3.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "40.56036000", Longitude = "20.25477000", Name = "Bashkia Skrapar" });
//            cities3.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "40.70583000", Longitude = "19.95222000", Name = "Berat" });
//            cities3.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "40.80028000", Longitude = "19.91667000", Name = "Kuçovë" });
//            cities3.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "40.61222000", Longitude = "20.09806000", Name = "Poliçan" });
//            cities3.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "40.66667000", Longitude = "20.00000000", Name = "Rrethi i Beratit" });
//            cities3.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "40.83333000", Longitude = "19.91667000", Name = "Rrethi i Kuçovës" });
//            cities3.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "40.55000000", Longitude = "20.26667000", Name = "Rrethi i Skraparit" });
//            cities3.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "40.76889000", Longitude = "19.87778000", Name = "Ura Vajgurore" });
//            State state3 = new State();
//            state3.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state3.Name = "Anambra State";
//            state3.CreatedDate = DateTime.Now;
//            ctx.States.Add(state3);
//            ctx.SaveChanges();
//            foreach (var item in cities3)
//            {
//                item.StateId = state3.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }


//            List<City> cities4 = new List<City>();
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.67478000", Longitude = "10.19069000", Name = "Azare" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.31032000", Longitude = "9.84388000", Name = "Bauchi" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.56109000", Longitude = "9.50154000", Name = "Boi" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.88224000", Longitude = "9.68058000", Name = "Bununu" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.99920000", Longitude = "10.41062000", Name = "Darazo" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.00065000", Longitude = "9.51596000", Name = "Dass" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.22629000", Longitude = "10.15132000", Name = "Dindima" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.48135000", Longitude = "9.91903000", Name = "Disina" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.10930000", Longitude = "10.44410000", Name = "Gabarin" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.23295000", Longitude = "10.28572000", Name = "Gwaram" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.24710000", Longitude = "10.56100000", Name = "Kari" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.44154000", Longitude = "9.23955000", Name = "Lame" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.71052000", Longitude = "9.34029000", Name = "Lere" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.44960000", Longitude = "10.36720000", Name = "Zadawa" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.77980000", Longitude = "10.44790000", Name = "Madara" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.31370000", Longitude = "10.46664000", Name = "Misau" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.35950000", Longitude = "10.67320000", Name = "Sade" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.11161000", Longitude = "9.82604000", Name = "Yamrat" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.50710000", Longitude = "10.74590000", Name = "Yanda Bayo" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.69707000", Longitude = "10.27350000", Name = "Yuli" });
//            cities4.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.61432000", Longitude = "10.17647000", Name = "Zalanga" });
//            State state4 = new State();
//            state4.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state4.Name = "Bauchi State";
//            state4.CreatedDate = DateTime.Now;
//            ctx.States.Add(state4);
//            ctx.SaveChanges();
//            foreach (var item in cities4)
//            {
//                item.StateId = state4.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }


//            List<City> cities5 = new List<City>();
//            cities5.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "4.97032000", Longitude = "6.10915000", Name = "Amassoma" });
//            cities5.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "4.31231000", Longitude = "6.24091000", Name = "Twon-Brass" });
//            cities5.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "4.92675000", Longitude = "6.26764000", Name = "Yenagoa" });
//            State state5 = new State();
//            state5.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state5.Name = "Bayelsa State";
//            state5.CreatedDate = DateTime.Now;
//            ctx.States.Add(state5);
//            ctx.SaveChanges();
//            foreach (var item in cities5)
//            {
//                item.StateId = state5.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities6 = new List<City>();
//            cities6.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.29627000", Longitude = "8.48278000", Name = "Aliade" });
//            cities6.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.35572000", Longitude = "7.89303000", Name = "Boju" });
//            cities6.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.45123000", Longitude = "8.60805000", Name = "Igbor" });
//            cities6.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.73375000", Longitude = "8.52139000", Name = "Makurdi" });
//            cities6.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.18045000", Longitude = "7.98240000", Name = "Ochobo" });
//            cities6.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.10168000", Longitude = "7.65945000", Name = "Otukpa" });
//            cities6.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.26667000", Longitude = "9.98333000", Name = "Takum" });
//            cities6.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.65321000", Longitude = "7.88410000", Name = "Ugbokpo" });
//            cities6.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.36308000", Longitude = "9.04235000", Name = "Yandev" });
//            cities6.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.50671000", Longitude = "9.61040000", Name = "Zaki Biam" });
//            State state6 = new State();
//            state6.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state6.Name = "Benue State";
//            state6.CreatedDate = DateTime.Now;
//            ctx.States.Add(state6);
//            ctx.SaveChanges();
//            foreach (var item in cities6)
//            {
//                item.StateId = state6.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities7 = new List<City>();
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.52134000", Longitude = "13.68952000", Name = "Bama" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.80919000", Longitude = "12.49151000", Name = "Benisheikh" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.61285000", Longitude = "12.19458000", Name = "Biu" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.27503000", Longitude = "12.56856000", Name = "Bornu Yassu" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "13.10518000", Longitude = "12.50854000", Name = "Damasak" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.15534000", Longitude = "12.75638000", Name = "Damboa" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.03609000", Longitude = "13.91815000", Name = "Dikwa" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.37299000", Longitude = "14.20690000", Name = "Gamboru" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.08313000", Longitude = "13.69595000", Name = "Gwoza" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.92475000", Longitude = "13.56617000", Name = "Kukawa" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.11451000", Longitude = "12.82620000", Name = "Magumeri" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.84692000", Longitude = "13.15712000", Name = "Maiduguri" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.36532000", Longitude = "13.82930000", Name = "Marte" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.73115000", Longitude = "12.14626000", Name = "Miringa" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.67059000", Longitude = "13.61224000", Name = "Monguno" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.34053000", Longitude = "14.18670000", Name = "Ngala" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.50673000", Longitude = "12.33315000", Name = "Shaffa" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.21824000", Longitude = "12.06059000", Name = "Shani" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.22135000", Longitude = "13.48783000", Name = "Tokombere" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.45509000", Longitude = "13.22233000", Name = "Uba" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.38522000", Longitude = "11.69678000", Name = "Wuyo" });
//            cities7.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.38623000", Longitude = "12.71992000", Name = "Yajiwa" });
//            State state7 = new State();
//            state7.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state7.Name = "Borno State";
//            state7.CreatedDate = DateTime.Now;
//            ctx.States.Add(state7);
//            ctx.SaveChanges();
//            foreach (var item in cities7)
//            {
//                item.StateId = state7.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }



//            List<City> cities8 = new List<City>();
//            cities8.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.08303000", Longitude = "11.49050000", Name = "Bankim" });
//            cities8.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.75000000", Longitude = "11.81667000", Name = "Banyo" });
//            cities8.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.05000000", Longitude = "14.43333000", Name = "Bélel" });
//            cities8.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.83333000", Longitude = "14.70000000", Name = "Djohong" });
//            cities8.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.96667000", Longitude = "12.23333000", Name = "Kontcha" });
//            cities8.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.58138000", Longitude = "11.73522000", Name = "Mayo-Banyo" });
//            cities8.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.51667000", Longitude = "14.30000000", Name = "Meïganga" });
//            cities8.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.32765000", Longitude = "13.58472000", Name = "Ngaoundéré" });
//            cities8.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.45843000", Longitude = "11.43299000", Name = "Somié" });
//            cities8.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.46504000", Longitude = "12.62843000", Name = "Tibati" });
//            cities8.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.36667000", Longitude = "12.65000000", Name = "Tignère" });
//            cities8.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.16365000", Longitude = "13.72711000", Name = "Vina" });
//            State state8 = new State();
//            state8.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state8.Name = "Adamawa State";
//            state8.CreatedDate = DateTime.Now;
//            ctx.States.Add(state8);
//            ctx.SaveChanges();
//            foreach (var item in cities8)
//            {
//                item.StateId = state8.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }


//            List<City> cities9 = new List<City>();
//            cities9.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.12640000", Longitude = "8.18980000", Name = "Akankpa" });
//            cities9.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "4.95893000", Longitude = "8.32695000", Name = "Calabar" });
//            cities9.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.76963000", Longitude = "8.99120000", Name = "Gakem" });
//            cities9.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "4.78978000", Longitude = "8.53160000", Name = "Ikang" });
//            cities9.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.80865000", Longitude = "8.08098000", Name = "Ugep" });
//            State state9 = new State();
//            state9.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state9.Name = "Cross River State";
//            state9.CreatedDate = DateTime.Now;
//            ctx.States.Add(state9);
//            ctx.SaveChanges();
//            foreach (var item in cities9)
//            {
//                item.StateId = state9.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities10 = new List<City>();
//            cities10.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.79023000", Longitude = "6.10473000", Name = "Abraka" });
//            cities10.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.25375000", Longitude = "6.19420000", Name = "Agbor" });
//            cities10.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.19824000", Longitude = "6.73187000", Name = "Asaba" });
//            cities10.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.16073000", Longitude = "5.92375000", Name = "Bomadi" });
//            cities10.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.35328000", Longitude = "5.50826000", Name = "Burutu" });
//            cities10.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.70773000", Longitude = "6.43402000", Name = "Kwale" });
//            cities10.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.84672000", Longitude = "6.15290000", Name = "Obiaruku" });
//            cities10.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.17811000", Longitude = "6.52461000", Name = "Ogwashi-Uku" });
//            cities10.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.63747000", Longitude = "5.89013000", Name = "Orerokpe" });
//            cities10.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.22885000", Longitude = "6.19139000", Name = "Patani" });
//            cities10.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.89405000", Longitude = "5.67666000", Name = "Sapele" });
//            cities10.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.48956000", Longitude = "6.00407000", Name = "Ughelli" });
//            cities10.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.26549000", Longitude = "6.30962000", Name = "Umunede" });
//            cities10.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.51737000", Longitude = "5.75006000", Name = "Warri" });
//            State state10 = new State();
//            state10.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state10.Name = "Delta State";
//            state10.CreatedDate = DateTime.Now;
//            ctx.States.Add(state10);
//            ctx.SaveChanges();
//            foreach (var item in cities10)
//            {
//                item.StateId = state10.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities11 = new List<City>();
//            cities11.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.32485000", Longitude = "8.11368000", Name = "Abakaliki" });
//            cities11.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.89258000", Longitude = "7.93534000", Name = "Afikpo" });
//            cities11.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.63105000", Longitude = "8.05814000", Name = "Effium" });
//            cities11.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.44094000", Longitude = "8.08432000", Name = "Ezza-Ohu" });
//            cities11.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.38186000", Longitude = "8.03736000", Name = "Isieke" });
//            State state11 = new State();
//            state11.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state11.Name = "Ebonyi State";
//            state11.CreatedDate = DateTime.Now;
//            ctx.States.Add(state11);
//            ctx.SaveChanges();
//            foreach (var item in cities11)
//            {
//                item.StateId = state11.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities12 = new List<City>();
//            cities12.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.10512000", Longitude = "6.69381000", Name = "Agenebode" });
//            cities12.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.06756000", Longitude = "6.26360000", Name = "Auchi" });
//            cities12.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.33815000", Longitude = "5.62575000", Name = "Benin City" });
//            cities12.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.74300000", Longitude = "6.14029000", Name = "Ekpoma" });
//            cities12.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.29366000", Longitude = "6.10432000", Name = "Igarra" });
//            cities12.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.67033000", Longitude = "6.62907000", Name = "Illushi" });
//            cities12.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.53589000", Longitude = "5.16005000", Name = "Siluko" });
//            cities12.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.65581000", Longitude = "6.38494000", Name = "Ubiaja" });
//            cities12.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.70000000", Longitude = "6.33333000", Name = "Uromi" });
//            State state12 = new State();
//            state12.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state12.Name = "Edo State";
//            state12.CreatedDate = DateTime.Now;
//            ctx.States.Add(state12);
//            ctx.SaveChanges();
//            foreach (var item in cities12)
//            {
//                item.StateId = state12.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities13 = new List<City>();
//            cities13.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.62329000", Longitude = "5.22087000", Name = "Ado-Ekiti" });
//            cities13.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.70483000", Longitude = "5.04054000", Name = "Aramoko-Ekiti" });
//            cities13.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.65649000", Longitude = "4.92235000", Name = "Efon-Alaaye" });
//            cities13.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.43636000", Longitude = "5.45925000", Name = "Emure-Ekiti" });
//            cities13.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.78942000", Longitude = "5.24852000", Name = "Ifaki" });
//            cities13.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.50251000", Longitude = "5.06258000", Name = "Igbara-Odo" });
//            cities13.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.66850000", Longitude = "5.12627000", Name = "Igede-Ekiti" });
//            cities13.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.81514000", Longitude = "5.06716000", Name = "Ijero-Ekiti" });
//            cities13.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.49748000", Longitude = "5.23041000", Name = "Ikere-Ekiti" });
//            cities13.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.87377000", Longitude = "5.07691000", Name = "Ipoti" });
//            cities13.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.46478000", Longitude = "5.42333000", Name = "Ise-Ekiti" });
//            cities13.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.95000000", Longitude = "4.98333000", Name = "Oke Ila" });
//            cities13.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.75833000", Longitude = "5.72227000", Name = "Omuo-Ekiti" });
//            State state13 = new State();
//            state13.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state13.Name = "Ekiti State";
//            state13.CreatedDate = DateTime.Now;
//            ctx.States.Add(state13);
//            ctx.SaveChanges();
//            foreach (var item in cities13)
//            {
//                item.StateId = state13.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities14 = new List<City>();
//            cities14.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.73971000", Longitude = "7.01117000", Name = "Adani" });
//            cities14.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.91677000", Longitude = "7.67615000", Name = "Ake-Eze" });
//            cities14.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.70902000", Longitude = "7.31826000", Name = "Aku" });
//            cities14.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.33063000", Longitude = "7.65247000", Name = "Amagunze" });
//            cities14.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.07278000", Longitude = "7.47739000", Name = "Awgu" });
//            cities14.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.65915000", Longitude = "7.75961000", Name = "Eha Amufu" });
//            cities14.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.44132000", Longitude = "7.49883000", Name = "Enugu" });
//            cities14.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.98270000", Longitude = "7.45534000", Name = "Enugu-Ezike" });
//            cities14.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.08956000", Longitude = "7.45341000", Name = "Ete" });
//            cities14.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.77993000", Longitude = "7.71484000", Name = "Ikem" });
//            cities14.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.17310000", Longitude = "7.63017000", Name = "Mberubu" });
//            cities14.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.85783000", Longitude = "7.39577000", Name = "Nsukka" });
//            cities14.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.88333000", Longitude = "7.63333000", Name = "Obolo-Eke (1)" });
//            cities14.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.78223000", Longitude = "7.43319000", Name = "Opi" });
//            cities14.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.31592000", Longitude = "7.42086000", Name = "Udi" });
//            State state14 = new State();
//            state14.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state14.Name = "Enugu State";
//            state14.CreatedDate = DateTime.Now;
//            ctx.States.Add(state14);
//            ctx.SaveChanges();
//            foreach (var item in cities14)
//            {
//                item.StateId = state14.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }


//            List<City> cities15 = new List<City>();
//            cities15.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.28899000", Longitude = "10.97320000", Name = "Akko" });
//            cities15.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.37444000", Longitude = "10.72884000", Name = "Bara" });
//            cities15.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.86545000", Longitude = "11.22624000", Name = "Billiri" });
//            cities15.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.61667000", Longitude = "11.43333000", Name = "Dadiya" });
//            cities15.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.21187000", Longitude = "11.38710000", Name = "Deba" });
//            cities15.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.82379000", Longitude = "10.77221000", Name = "Dukku" });
//            cities15.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.17506000", Longitude = "11.16458000", Name = "Garko" });
//            cities15.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.28969000", Longitude = "11.16729000", Name = "Gombe" });
//            cities15.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.30426000", Longitude = "11.49905000", Name = "Hinna" });
//            cities15.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.38304000", Longitude = "11.09567000", Name = "Kafarati" });
//            cities15.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.81998000", Longitude = "11.30871000", Name = "Kaltungo" });
//            cities15.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.04807000", Longitude = "11.21055000", Name = "Kumo" });
//            cities15.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.09596000", Longitude = "11.33261000", Name = "Nafada" });
//            cities15.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.98433000", Longitude = "10.95229000", Name = "Pindiga" });
//            State state15 = new State();
//            state15.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state15.Name = "Gombe State";
//            state15.CreatedDate = DateTime.Now;
//            ctx.States.Add(state15);
//            ctx.SaveChanges();
//            foreach (var item in cities15)
//            {
//                item.StateId = state15.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }


//            List<City> cities16 = new List<City>();
//            cities16.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.58225000", Longitude = "7.09896000", Name = "Iho" });
//            cities16.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.71044000", Longitude = "6.80936000", Name = "Oguta" });
//            cities16.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.82917000", Longitude = "7.35056000", Name = "Okigwe" });
//            cities16.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.79565000", Longitude = "7.03513000", Name = "Orlu" });
//            cities16.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.61667000", Longitude = "7.03333000", Name = "Orodo" });
//            cities16.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "5.48363000", Longitude = "7.03325000", Name = "Owerri" });
//            State state16 = new State();
//            state16.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state16.Name = "Imo State";
//            state16.CreatedDate = DateTime.Now;
//            ctx.States.Add(state16);
//            ctx.SaveChanges();
//            foreach (var item in cities16)
//            {
//                item.StateId = state16.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities17 = new List<City>();
//            cities17.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.77256000", Longitude = "9.01525000", Name = "Babura" });
//            cities17.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.45207000", Longitude = "9.47856000", Name = "Birnin Kudu" });
//            cities17.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.79070000", Longitude = "10.23614000", Name = "Birniwa" });
//            cities17.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.75618000", Longitude = "9.33896000", Name = "Dutse" });
//            cities17.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.40848000", Longitude = "9.52881000", Name = "Gagarawa" });
//            cities17.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.62690000", Longitude = "9.38807000", Name = "Gumel" });
//            cities17.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.27727000", Longitude = "9.88385000", Name = "Gwaram" });
//            cities17.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.45347000", Longitude = "10.04115000", Name = "Hadejia" });
//            cities17.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.23933000", Longitude = "9.91105000", Name = "Kafin Hausa" });
//            cities17.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.64846000", Longitude = "8.41178000", Name = "Kazaure" });
//            cities17.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.78442000", Longitude = "9.60690000", Name = "Kiyawa" });
//            cities17.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.56427000", Longitude = "9.95727000", Name = "Mallammaduri" });
//            cities17.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.15143000", Longitude = "9.16216000", Name = "Ringim" });
//            cities17.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.34873000", Longitude = "9.63989000", Name = "Samamiya" });
//            State state17 = new State();
//            state17.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state17.Name = "Jigawa State";
//            state17.CreatedDate = DateTime.Now;
//            ctx.States.Add(state17);
//            ctx.SaveChanges();
//            foreach (var item in cities17)
//            {
//                item.StateId = state17.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities18 = new List<City>();
//            cities18.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.96245000", Longitude = "8.39233000", Name = "Anchau" });
//            cities18.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.39106000", Longitude = "8.72341000", Name = "Burumburum" });
//            cities18.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.85009000", Longitude = "8.19900000", Name = "Dutsen Wai" });
//            cities18.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.26680000", Longitude = "7.64916000", Name = "Hunkuyi" });
//            cities18.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.87342000", Longitude = "7.95407000", Name = "Kachia" });
//            cities18.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.52641000", Longitude = "7.43879000", Name = "Kaduna" });
//            cities18.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.58126000", Longitude = "8.29260000", Name = "Kafanchan" });
//            cities18.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.60776000", Longitude = "8.39043000", Name = "Kagoro" });
//            cities18.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.87342000", Longitude = "7.95407000", Name = "Kachia" });
//            cities18.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.32281000", Longitude = "7.68462000", Name = "Kajuru" });
//            cities18.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.45767000", Longitude = "7.63808000", Name = "Kujama" });
//            cities18.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.38584000", Longitude = "8.57286000", Name = "Lere" });
//            cities18.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.71667000", Longitude = "6.56667000", Name = "Mando" });
//            cities18.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.41227000", Longitude = "8.68748000", Name = "Saminaka" });
//            cities18.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.98133000", Longitude = "8.05749000", Name = "Soba" });
//            cities18.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.01537000", Longitude = "6.78036000", Name = "Sofo-Birnin-Gwari" });
//            cities18.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.11128000", Longitude = "7.72270000", Name = "Zaria" });
//            State state18 = new State();
//            state18.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state18.Name = "Kaduna State";
//            state18.CreatedDate = DateTime.Now;
//            ctx.States.Add(state18);
//            ctx.SaveChanges();
//            foreach (var item in cities18)
//            {
//                item.StateId = state18.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }


//            List<City> cities19 = new List<City>();
//            cities19.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.53485000", Longitude = "8.15224000", Name = "Dan Gora" });
//            cities19.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.86064000", Longitude = "9.00270000", Name = "Gaya" });
//            cities19.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.00012000", Longitude = "8.51672000", Name = "Kano" });
//            State state19 = new State();
//            state19.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state19.Name = "Kano State";
//            state19.CreatedDate = DateTime.Now;
//            ctx.States.Add(state19);
//            ctx.SaveChanges();
//            foreach (var item in cities19)
//            {
//                item.StateId = state19.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities20 = new List<City>();
//            cities20.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.37710000", Longitude = "7.56097000", Name = "Danja" });
//            cities20.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "13.29782000", Longitude = "7.79492000", Name = "Dankama" });
//            cities20.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "13.03299000", Longitude = "8.32351000", Name = "Daura" });
//            cities20.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.45392000", Longitude = "7.49723000", Name = "Dutsin-Ma" });
//            cities20.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.47196000", Longitude = "7.30699000", Name = "Funtua" });
//            cities20.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.91294000", Longitude = "7.66531000", Name = "Gora" });
//            cities20.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "13.09378000", Longitude = "7.22624000", Name = "Jibia" });
//            cities20.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.17328000", Longitude = "7.77424000", Name = "Jikamshi" });
//            cities20.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.93114000", Longitude = "7.41115000", Name = "Kankara" });
//            cities20.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.99082000", Longitude = "7.60177000", Name = "Katsina" });
//            cities20.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.98044000", Longitude = "7.94703000", Name = "Mashi" });
//            cities20.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.86260000", Longitude = "7.23469000", Name = "Ruma" });
//            cities20.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.44788000", Longitude = "7.30918000", Name = "Runka" });
//            cities20.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.68781000", Longitude = "7.19579000", Name = "Wagini" });
//            State state20 = new State();
//            state20.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state20.Name = "Katsina State";
//            state20.CreatedDate = DateTime.Now;
//            ctx.States.Add(state20);
//            ctx.SaveChanges();
//            foreach (var item in cities20)
//            {
//                item.StateId = state20.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities21 = new List<City>();
//            cities21.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.74482000", Longitude = "4.52514000", Name = "Argungu" });
//            cities21.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.40351000", Longitude = "4.22571000", Name = "Bagudo" });
//            cities21.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.28444000", Longitude = "5.93472000", Name = "Bena" });
//            cities21.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.78230000", Longitude = "4.81135000", Name = "Bin Yauri" });
//            cities21.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.45389000", Longitude = "4.19750000", Name = "Birnin Kebbi" });
//            cities21.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.64809000", Longitude = "4.06177000", Name = "Dakingari" });
//            cities21.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.64231000", Longitude = "4.35545000", Name = "Gulma" });
//            cities21.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.50204000", Longitude = "4.64295000", Name = "Gwandu" });
//            cities21.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.22336000", Longitude = "4.37971000", Name = "Jega" });
//            cities21.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.85172000", Longitude = "3.65478000", Name = "Kamba" });
//            cities21.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.55339000", Longitude = "3.81814000", Name = "Kangiwa" });
//            cities21.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.51966000", Longitude = "4.26030000", Name = "Kende" });
//            cities21.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "-10.86667000", Longitude = "39.45000000", Name = "Mahuta" });
//            cities21.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.08225000", Longitude = "4.36907000", Name = "Maiyama" });
//            cities21.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.21374000", Longitude = "4.57941000", Name = "Shanga" });
//            cities21.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.37640000", Longitude = "5.79536000", Name = "Wasagu" });
//            cities21.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.43522000", Longitude = "5.23494000", Name = "Zuru" });
//            State state21 = new State();
//            state21.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state21.Name = "Kebbi State";
//            state21.CreatedDate = DateTime.Now;
//            ctx.States.Add(state21);
//            ctx.SaveChanges();
//            foreach (var item in cities21)
//            {
//                item.StateId = state21.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities22 = new List<City>();
//            cities22.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.56770000", Longitude = "6.98630000", Name = "Abocho" });
//            cities22.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.97694000", Longitude = "7.16262000", Name = "Adoru" });
//            cities22.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.40249000", Longitude = "7.63196000", Name = "Ankpa" });
//            cities22.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.99917000", Longitude = "7.58361000", Name = "Bugana" });
//            cities22.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.68967000", Longitude = "7.04380000", Name = "Dekina" });
//            cities22.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.21667000", Longitude = "5.51667000", Name = "Egbe" });
//            cities22.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.70670000", Longitude = "6.77180000", Name = "Icheu" });
//            cities22.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.11345000", Longitude = "6.73866000", Name = "Idah" });
//            cities22.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.27445000", Longitude = "5.83528000", Name = "Isanlu-Itedoijowa" });
//            cities22.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.09120000", Longitude = "6.79782000", Name = "Koton-Karfe" });
//            cities22.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.79688000", Longitude = "6.74048000", Name = "Lokoja" });
//            cities22.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.59383000", Longitude = "6.21798000", Name = "Ogaminana" });
//            cities22.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.78636000", Longitude = "6.95017000", Name = "Ogurugu" });
//            cities22.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.55122000", Longitude = "6.23589000", Name = "Okene" });
//            State state22 = new State();
//            state22.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state22.Name = "Kogi State";
//            state22.CreatedDate = DateTime.Now;
//            ctx.States.Add(state22);
//            ctx.SaveChanges();
//            foreach (var item in cities22)
//            {
//                item.StateId = state22.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities23 = new List<City>();
//            cities23.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.23333000", Longitude = "4.81667000", Name = "Ajasse Ipo" });
//            cities23.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.93900000", Longitude = "4.78227000", Name = "Bode Saadu" });
//            cities23.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.48333000", Longitude = "3.50000000", Name = "Gwasero" });
//            cities23.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.49664000", Longitude = "4.54214000", Name = "Ilorin" });
//            cities23.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.11972000", Longitude = "4.82360000", Name = "Jebba" });
//            cities23.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.60530000", Longitude = "3.94101000", Name = "Kaiama" });
//            cities23.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.85299000", Longitude = "5.41641000", Name = "Lafiagi" });
//            cities23.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.14911000", Longitude = "4.72074000", Name = "Offa" });
//            cities23.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.21667000", Longitude = "3.18333000", Name = "Okuta" });
//            cities23.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.13857000", Longitude = "5.10260000", Name = "Omu-Aran" });
//            cities23.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.72851000", Longitude = "5.75561000", Name = "Patigi" });
//            cities23.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.46667000", Longitude = "3.18333000", Name = "Suya" });
//            cities23.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.76667000", Longitude = "3.40000000", Name = "Yashikera" });
//            State state23 = new State();
//            state23.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state23.Name = "Kano State";
//            state23.CreatedDate = DateTime.Now;
//            ctx.States.Add(state23);
//            ctx.SaveChanges();
//            foreach (var item in cities23)
//            {
//                item.StateId = state23.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }


//            List<City> cities24 = new List<City>();
//            cities24.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.49056000", Longitude = "7.34139000", Name = "Buga" });
//            cities24.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.39307000", Longitude = "8.35544000", Name = "Doma" });
//            cities24.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.84651000", Longitude = "7.87354000", Name = "Keffi" });
//            cities24.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.49390000", Longitude = "8.51532000", Name = "Lafia" });
//            cities24.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.53895000", Longitude = "7.70821000", Name = "Nasarawa" });
//            cities24.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.94153000", Longitude = "8.60315000", Name = "Wamba" });
//            State state24 = new State();
//            state24.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state24.Name = "Nasarawa State";
//            state24.CreatedDate = DateTime.Now;
//            ctx.States.Add(state24);
//            ctx.SaveChanges();
//            foreach (var item in cities24)
//            {
//                item.StateId = state24.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities25 = new List<City>();
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.18805000", Longitude = "4.72318000", Name = "Auna" });
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.42949000", Longitude = "3.81495000", Name = "Babana" });
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.05630000", Longitude = "6.14300000", Name = "Badeggi" });
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.61565000", Longitude = "6.41850000", Name = "Baro" });
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.43333000", Longitude = "5.20000000", Name = "Bokani" });
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.23610000", Longitude = "4.90727000", Name = "Duku" });
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.48536000", Longitude = "5.14501000", Name = "Ibeto" });
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.84686000", Longitude = "4.09835000", Name = "Konkwesso" });
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.40319000", Longitude = "5.47080000", Name = "Kontagora" });
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.53283000", Longitude = "6.44222000", Name = "Kusheriki" });
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.86864000", Longitude = "6.71042000", Name = "Kuta" });
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.04439000", Longitude = "6.57089000", Name = "Lapai" });
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.61524000", Longitude = "6.54776000", Name = "Minna" });
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.33957000", Longitude = "4.46880000", Name = "New Shagunnu" });
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.18059000", Longitude = "7.17939000", Name = "Suleja" });
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.07060000", Longitude = "6.19060000", Name = "Tegina" });
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "10.83122000", Longitude = "5.82494000", Name = "Ukata" });
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.90222000", Longitude = "4.41917000", Name = "Wawa" });
//            cities25.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.80726000", Longitude = "6.15238000", Name = "Zungeru" });
//            State state25 = new State();
//            state25.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state25.Name = "Niger State";
//            state25.CreatedDate = DateTime.Now;
//            ctx.States.Add(state25);
//            ctx.SaveChanges();
//            foreach (var item in cities25)
//            {
//                item.StateId = state25.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities26 = new List<City>();
//            cities26.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.15571000", Longitude = "3.34509000", Name = "Abeokuta" });
//            cities26.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.60000000", Longitude = "2.93333000", Name = "Ado Odo" });
//            cities26.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.63333000", Longitude = "2.73333000", Name = "Idi Iroko" });
//            cities26.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.81491000", Longitude = "3.19518000", Name = "Ifo" });
//            cities26.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.77837000", Longitude = "4.03386000", Name = "Ijebu-Ife" });
//            cities26.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.97198000", Longitude = "3.99938000", Name = "Ijebu-Igbo" });
//            cities26.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.81944000", Longitude = "3.91731000", Name = "Ijebu-Ode" });
//            cities26.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.88901000", Longitude = "3.01416000", Name = "Ilaro" });
//            cities26.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.44888000", Longitude = "2.84289000", Name = "Imeko" });
//            cities26.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.91002000", Longitude = "3.66557000", Name = "Iperu" });
//            cities26.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.99345000", Longitude = "3.68148000", Name = "Isara" });
//            cities26.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.94851000", Longitude = "3.50561000", Name = "Owode" });
//            State state26 = new State();
//            state26.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state26.Name = "Ogun State";
//            state26.CreatedDate = DateTime.Now;
//            ctx.States.Add(state26);
//            ctx.SaveChanges();
//            foreach (var item in cities26)
//            {
//                item.StateId = state26.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities27 = new List<City>();
//            cities27.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.35156000", Longitude = "4.18335000", Name = "Apomu" });
//            cities27.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.90292000", Longitude = "4.31419000", Name = "Ejigbo" });
//            cities27.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.47734000", Longitude = "4.35351000", Name = "Gbongan" });
//            cities27.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.68287000", Longitude = "4.81769000", Name = "Ijebu-Jesa" });
//            cities27.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.36983000", Longitude = "4.18630000", Name = "Ikire" });
//            cities27.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.91283000", Longitude = "4.66741000", Name = "Ikirun" });
//            cities27.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.01714000", Longitude = "4.90421000", Name = "Ila Orangun" });
//            cities27.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.48240000", Longitude = "4.56032000", Name = "Ile-Ife" });
//            cities27.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.62789000", Longitude = "4.74161000", Name = "Ilesa" });
//            cities27.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.84036000", Longitude = "4.48557000", Name = "Ilobu" });
//            cities27.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.85000000", Longitude = "4.33333000", Name = "Inisa" });
//            cities27.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.63527000", Longitude = "4.18156000", Name = "Iwo" });
//            cities27.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.38299000", Longitude = "4.26031000", Name = "Modakeke" });
//            cities27.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.81667000", Longitude = "4.91667000", Name = "Oke Mesi" });
//            cities27.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.60000000", Longitude = "4.18333000", Name = "Olupona" });
//            cities27.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.77104000", Longitude = "4.55698000", Name = "Osogbo" });
//            cities27.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.94783000", Longitude = "4.78836000", Name = "Otan Ayegbaju" });
//            cities27.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.05000000", Longitude = "4.76667000", Name = "Oyan" });
//            State state27 = new State();
//            state27.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state27.Name = "Osun State";
//            state27.CreatedDate = DateTime.Now;
//            ctx.States.Add(state27);
//            ctx.SaveChanges();
//            foreach (var item in cities27)
//            {
//                item.StateId = state27.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities28 = new List<City>();
//            cities28.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.50000000", Longitude = "3.41667000", Name = "Ago Are" });
//            cities28.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.61667000", Longitude = "4.38333000", Name = "Alapa" });
//            cities28.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.71361000", Longitude = "3.91722000", Name = "Fiditi" });
//            cities28.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.37756000", Longitude = "3.90591000", Name = "Ibadan" });
//            cities28.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.74921000", Longitude = "4.13113000", Name = "Igbeti" });
//            cities28.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.43383000", Longitude = "3.28788000", Name = "Igbo-Ora" });
//            cities28.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.83784000", Longitude = "3.75628000", Name = "Igboho" });
//            cities28.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.08297000", Longitude = "3.85196000", Name = "Kisi" });
//            cities28.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.46791000", Longitude = "4.06594000", Name = "Lalupon" });
//            cities28.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.13373000", Longitude = "4.24014000", Name = "Ogbomoso" });
//            cities28.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.03386000", Longitude = "3.34759000", Name = "Okeho" });
//            cities28.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.55000000", Longitude = "3.43333000", Name = "Orita Eruwa" });
//            cities28.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.85257000", Longitude = "3.93125000", Name = "Oyo" });
//            cities28.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.66762000", Longitude = "3.39393000", Name = "Saki" });
//            State state28 = new State();
//            state28.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state28.Name = "Oyo State";
//            state28.CreatedDate = DateTime.Now;
//            ctx.States.Add(state28);
//            ctx.SaveChanges();
//            foreach (var item in cities28)
//            {
//                item.StateId = state28.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities29 = new List<City>();
//            cities29.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.35509000", Longitude = "9.70121000", Name = "Amper" });
//            cities29.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.79399000", Longitude = "8.86397000", Name = "Bukuru" });
//            cities29.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.36872000", Longitude = "9.96223000", Name = "Dengi" });
//            cities29.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.92849000", Longitude = "8.89212000", Name = "Jos" });
//            cities29.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.90361000", Longitude = "9.29086000", Name = "Kwolla" });
//            cities29.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.14164000", Longitude = "9.79101000", Name = "Langtang" });
//            cities29.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.32541000", Longitude = "9.43520000", Name = "Pankshin" });
//            cities29.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.40756000", Longitude = "9.21481000", Name = "Panyam" });
//            cities29.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.72910000", Longitude = "8.79138000", Name = "Vom" });
//            cities29.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.83333000", Longitude = "9.63333000", Name = "Yelwa" });
//            State state29 = new State();
//            state29.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state29.Name = "Plateau State";
//            state29.CreatedDate = DateTime.Now;
//            ctx.States.Add(state29);
//            ctx.SaveChanges();
//            foreach (var item in cities29)
//            {
//                item.StateId = state29.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }


//            List<City> cities30 = new List<City>();
//            cities30.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "13.22294000", Longitude = "4.90888000", Name = "Binji" });
//            cities30.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.85313000", Longitude = "5.34572000", Name = "Dange" });
//            cities30.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.96358000", Longitude = "5.74337000", Name = "Gandi" });
//            cities30.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "13.44226000", Longitude = "5.67234000", Name = "Goronyo" });
//            cities30.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "13.35819000", Longitude = "5.23812000", Name = "Gwadabawa" });
//            cities30.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "13.73064000", Longitude = "5.29777000", Name = "Illela" });
//            cities30.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "13.12257000", Longitude = "5.50762000", Name = "Rabah" });
//            cities30.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "13.06269000", Longitude = "5.24322000", Name = "Sokoto" });
//            cities30.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.40592000", Longitude = "4.64605000", Name = "Tambuwal" });
//            cities30.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "13.29048000", Longitude = "5.42373000", Name = "Wurno" });
//            State state30 = new State();
//            state30.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state30.Name = "Sokoto State";
//            state30.CreatedDate = DateTime.Now;
//            ctx.States.Add(state30);
//            ctx.SaveChanges();
//            foreach (var item in cities30)
//            {
//                item.StateId = state30.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities31 = new List<City>();
//            cities31.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.23087000", Longitude = "10.62444000", Name = "Baissa" });
//            cities31.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.85868000", Longitude = "10.97187000", Name = "Beli" });
//            cities31.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.53535000", Longitude = "10.44615000", Name = "Gassol" });
//            cities31.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "6.72556000", Longitude = "11.25652000", Name = "Gembu" });
//            cities31.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.18122000", Longitude = "9.74431000", Name = "Ibi" });
//            cities31.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.89367000", Longitude = "11.35960000", Name = "Jalingo" });
//            cities31.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "9.20827000", Longitude = "11.27541000", Name = "Lau" });
//            cities31.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.90844000", Longitude = "9.61688000", Name = "Riti" });
//            cities31.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "8.64138000", Longitude = "10.77355000", Name = "Mutum Biyu" });
//            cities31.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "7.87139000", Longitude = "9.77786000", Name = "Wukari" });
//            State state31 = new State();
//            state31.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state31.Name = "Taraba State";
//            state31.CreatedDate = DateTime.Now;
//            ctx.States.Add(state31);
//            ctx.SaveChanges();
//            foreach (var item in cities31)
//            {
//                item.StateId = state31.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities32 = new List<City>();
//            cities32.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.74697000", Longitude = "11.96083000", Name = "Damaturu" });
//            cities32.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.74449000", Longitude = "12.18545000", Name = "Dankalwa" });
//            cities32.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.49536000", Longitude = "11.49977000", Name = "Dapchi" });
//            cities32.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.55410000", Longitude = "11.40600000", Name = "Daura" });
//            cities32.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.37700000", Longitude = "11.23746000", Name = "Fika" });
//            cities32.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.87398000", Longitude = "11.04057000", Name = "Gashua" });
//            cities32.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.89439000", Longitude = "11.92649000", Name = "Geidam" });
//            cities32.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.48451000", Longitude = "12.31264000", Name = "Goniri" });
//            cities32.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.63958000", Longitude = "10.70422000", Name = "Gorgoram" });
//            cities32.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.49959000", Longitude = "11.93396000", Name = "Gujba" });
//            cities32.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "13.15498000", Longitude = "10.63468000", Name = "Kumagunnam" });
//            cities32.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.98022000", Longitude = "11.44002000", Name = "Lajere" });
//            cities32.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "13.13639000", Longitude = "10.04924000", Name = "Machina" });
//            cities32.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.67479000", Longitude = "11.06690000", Name = "Gwio Kura" });
//            cities32.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.71391000", Longitude = "11.08108000", Name = "Potiskum" });
//            cities32.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.87695000", Longitude = "10.45536000", Name = "Nguru" });
//            State state32 = new State();
//            state32.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state32.Name = "Yobe State";
//            state32.CreatedDate = DateTime.Now;
//            ctx.States.Add(state32);
//            ctx.SaveChanges();
//            foreach (var item in cities32)
//            {
//                item.StateId = state32.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }

//            List<City> cities33 = new List<City>();
//            cities33.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.11347000", Longitude = "5.92681000", Name = "Anka" });
//            cities33.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.29621000", Longitude = "6.49520000", Name = "Dan Sadau" });
//            cities33.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.14484000", Longitude = "5.11776000", Name = "Gummi" });
//            cities33.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.17024000", Longitude = "6.66412000", Name = "Gusau" });
//            cities33.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.59371000", Longitude = "6.58648000", Name = "Kaura Namoda" });
//            cities33.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.14082000", Longitude = "6.82196000", Name = "Kwatarkwashi" });
//            cities33.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.33360000", Longitude = "6.40372000", Name = "Maru" });
//            cities33.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "12.87405000", Longitude = "6.48754000", Name = "Moriki" });
//            cities33.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.72655000", Longitude = "6.78374000", Name = "Sauri" });
//            cities33.Add(new City() { CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, Latitude = "11.95775000", Longitude = "6.92083000", Name = "Tsafe" });
//            State state33 = new State();
//            state33.CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29";
//            state33.Name = "Zamfara State";
//            state33.CreatedDate = DateTime.Now;
//            ctx.States.Add(state33);
//            ctx.SaveChanges();
//            foreach (var item in cities33)
//            {
//                item.StateId = state33.Id;
//                ctx.Cities.Add(item);
//                ctx.SaveChanges();
//            }
//        }
//        private static void SeedProgramCompulsoryFee()
//        {
//            ctx.ProgramCompulsoryFee.Add(new ProgramCompulsoryFee() { School = "Undergraduate Programs", StartingLevel = 100, StartingSemester = 1, EndingLevel = 100, EndingSemester = 1, FeeAmount = 36000, CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now });
//            ctx.ProgramCompulsoryFee.Add(new ProgramCompulsoryFee() { School = "Undergraduate Programs", StartingLevel = 100, StartingSemester = 2, EndingLevel = 500, EndingSemester = 2, FeeAmount = 18000, CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now });

//            ctx.ProgramCompulsoryFee.Add(new ProgramCompulsoryFee() { School = "Master Programs", StartingLevel = 800, StartingSemester = 1, EndingLevel = 800, EndingSemester = 1, FeeAmount = 35000, CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now });
//            ctx.ProgramCompulsoryFee.Add(new ProgramCompulsoryFee() { School = "Master Programs", StartingLevel = 800, StartingSemester = 2, EndingLevel = 800, EndingSemester = 4, FeeAmount = 18000, CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now });

//            ctx.ProgramCompulsoryFee.Add(new ProgramCompulsoryFee() { School = "PostUndergraduate Programs", StartingLevel = 700, StartingSemester = 1, EndingLevel = 700, EndingSemester = 1, FeeAmount = 35000, CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now });
//            ctx.ProgramCompulsoryFee.Add(new ProgramCompulsoryFee() { School = "PostUndergraduate Programs", StartingLevel = 700, StartingSemester = 2, EndingLevel = 700, EndingSemester = 2, FeeAmount = 18000, CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now });

//        }
//        private static void SeedBookmarks()
//        {
//            ctx.Bookmarks.Add(new Bookmarks() { Title = "Google", Address = "https://www.google.com", CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, GuidKey = Guid.NewGuid().ToString(), IsDeleted = false });
//            ctx.Bookmarks.Add(new Bookmarks() { Title = "Stack Overflow", Address = "https://stackoverflow.com/", CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, GuidKey = Guid.NewGuid().ToString(), IsDeleted = false });
//            ctx.SaveChanges();
//        }
//        private static void SeedSecurityQuestions()
//        {
//            ctx.SecurityQuestions.Add(new SecurityQuestions() { Question = "What is your date of birth?", CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, GuidKey = Guid.NewGuid().ToString(), IsDeleted = false });
//            ctx.SecurityQuestions.Add(new SecurityQuestions() { Question = "What is your mother name?", CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, GuidKey = Guid.NewGuid().ToString(), IsDeleted = false });
//            ctx.SecurityQuestions.Add(new SecurityQuestions() { Question = "What is your father name?", CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, GuidKey = Guid.NewGuid().ToString(), IsDeleted = false });
//            ctx.SecurityQuestions.Add(new SecurityQuestions() { Question = "What is your childhood school?", CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, GuidKey = Guid.NewGuid().ToString(), IsDeleted = false });
//            ctx.SecurityQuestions.Add(new SecurityQuestions() { Question = "What is the name of your pet?", CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, GuidKey = Guid.NewGuid().ToString(), IsDeleted = false });
//            ctx.SaveChanges();
//        }

//        private static void SeedLicenses()
//        {
//            //Desktop
//            ctx.License.Add(new License() { Title = "Regular", LicenseMode = "Paid", NewLicenseFee = 0, RenewLicenseFee = 0, maxDeviceCount = 1, Description = "Regular License covers all package-1 features", LicenseType = "Desktop", CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, GuidKey = Guid.NewGuid().ToString(), IsDeleted = false });
//            ctx.License.Add(new License() { Title = "Premium", LicenseMode = "Paid", NewLicenseFee = 0, RenewLicenseFee = 0, maxDeviceCount = 1, Description = "Premium License covers all package-1 & package-2 features", LicenseType = "Desktop", CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, GuidKey = Guid.NewGuid().ToString(), IsDeleted = false });

//            //Desktop Promo Licenses
//            ctx.License.Add(new License() { Title = "Regular Promo", LicenseMode = "Paid", NewLicenseFee = 0, RenewLicenseFee = 0, maxDeviceCount = 2, Description = "Regular License covers all package-1 features", LicenseType = "Desktop", CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, GuidKey = Guid.NewGuid().ToString(), IsDeleted = false });
//            ctx.License.Add(new License() { Title = "Premium Promo", LicenseMode = "Paid", NewLicenseFee = 0, RenewLicenseFee = 0, maxDeviceCount = 2, Description = "Premium License covers all package-1 & package-2 features", LicenseType = "Desktop", CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, GuidKey = Guid.NewGuid().ToString(), IsDeleted = false });

//            //Mobile
//            ctx.License.Add(new License() { Title = "Free Mode", LicenseMode = "Free", NewLicenseFee = 0, RenewLicenseFee = 0, maxDeviceCount = 1, Description = "Free Mode covers timetable, notifications, fee analysis, get help/support & social media features only", LicenseType = "Android", CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, GuidKey = Guid.NewGuid().ToString(), IsDeleted = false });
//            ctx.License.Add(new License() { Title = "Paid License", LicenseMode = "Paid", NewLicenseFee = 0, RenewLicenseFee = 0, maxDeviceCount = 1, Description = "Paid License covers all features", LicenseType = "Android", CreatedBy = "28f204c4-e0ed-4bcd-bf99-a25c7d2a6a29", CreatedDate = DateTime.Now, GuidKey = Guid.NewGuid().ToString(), IsDeleted = false });
//            ctx.SaveChanges();
//        }
//    }
//}
