using System;

namespace BusinessLayer
{
    public class BusinessWrapper
    {
        private UserBL userBL;
        private CategoryBL programsBL;
        private AreaBL areaBL;
        private CategoryBL categoryBL;
        private BlogBL blogBL;
        private CusinieBL cusinieBL;
        public IServiceProvider _serviceProvider;
        public BusinessWrapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public UserBL UserBL
        {
            get
            {
                return userBL == null ? userBL = new UserBL(_serviceProvider) : userBL;
            }
        }

        public CusinieBL CusinieBL
        {
            get
            {
                return cusinieBL == null ? cusinieBL = new CusinieBL(_serviceProvider) : cusinieBL;
            }
        }


        public CategoryBL CategoryBL
        {
            get
            {
                return categoryBL == null ? categoryBL = new CategoryBL(_serviceProvider) : categoryBL;
            }
        }
        public BlogBL BlogBL
        {
            get
            {
                return blogBL == null ? blogBL = new BlogBL(_serviceProvider) : blogBL;
            }
        }


        public CategoryBL ProgramsBL
        {
            get
            {
                return programsBL == null ? programsBL = new CategoryBL(_serviceProvider) : programsBL;
            }
        }
       
        public AreaBL AreaBL
        {
            get
            {
                return areaBL == null ? areaBL = new AreaBL(_serviceProvider) : areaBL;
            }
        }
    }
    
}
