using CallCenter.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenter.BusinessLogic
{
    public class CategoryProvider
    {

        public List<TicketManagement> GetAllTickets()
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                List<TicketManagement> ticketManagements = new List<TicketManagement>();
                //ticketManagements = (from ticket in dbContext.TicketManagements select ticket).ToList();
                return ticketManagements;
            }
        }
        
        public TicketManagement GetTicketsById(int Id)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                TicketManagement ticketManagements = new TicketManagement();
                ticketManagements = (from ticket in dbContext.TicketManagements where ticket.TicketId== Id select ticket).SingleOrDefault();
                return ticketManagements;
            }
        }
        public SRManagement GetSRById(int Id)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                SRManagement sRManagement  = new SRManagement();
                sRManagement = (from sr in dbContext.SRManagements where sr.SRId == Id select sr).SingleOrDefault();
                return sRManagement;
            }
        }
        public List<SRManagement> GetAllSRs()
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                List<SRManagement> sRManagements = new List<SRManagement>();
                //sRManagements = (from sr in dbContext.SRManagements select sr).ToList();
                return sRManagements;
            }
        }
        public List<CategoryMaster> GetAllCategory()
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                List<CategoryMaster> categoryMasters = new List<CategoryMaster>();
                categoryMasters = (from cat in dbContext.CategoryMasters select cat).ToList();
                return categoryMasters;
            }
        }
        public CategoryMaster GetCategoryById(int CategoryId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                 CategoryMaster categoryMasters = new  CategoryMaster();
                categoryMasters = (from cat in dbContext.CategoryMasters where cat.CategoryId == CategoryId select cat).FirstOrDefault();
                return categoryMasters;
            }
        }

        public List<SubCategoryMaster> GetSubCategoryByCategoryId(int CategoryId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                List<SubCategoryMaster> subCategoryMasters = new List<SubCategoryMaster>();
                subCategoryMasters = (from subCat in dbContext.SubCategoryMasters
                                      where subCat.CategoryId == CategoryId
                                      select subCat).ToList();
                return subCategoryMasters;
            }
        }
        public List<SubSubCategoryMaster> GetSubSubCategoryBySubCategoryId(int subCategoryId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                List<SubSubCategoryMaster> subSubCategoryMasters = new List<SubSubCategoryMaster>();
                subSubCategoryMasters = (from subCat in dbContext.SubSubCategoryMasters
                                      where subCat.SubCategoryId == subCategoryId
                                      select subCat).ToList();
                return subSubCategoryMasters;
            }
        }

        public List<SubCategoryMaster> GetAllSubCategoryWithCategory()
        {

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                List<SubCategoryMaster> subCategoryMasters = new List<SubCategoryMaster>();
                subCategoryMasters = (from subCat in dbContext.SubCategoryMasters
                                      select subCat).ToList();
                foreach (var item in subCategoryMasters)
                {
                    item.CategoryName = (from cat in dbContext.CategoryMasters where cat.CategoryId == item.CategoryId select cat.CategoryName).SingleOrDefault();
                }
                return subCategoryMasters;
            }
        }
        public SubCategoryMaster GetSubCategoryById(int subCategoryId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                SubCategoryMaster subCategoryMasters = new SubCategoryMaster();
                subCategoryMasters = (from subCat in dbContext.SubCategoryMasters where subCat.SubCategoryId == subCategoryId select subCat).FirstOrDefault();
                return subCategoryMasters;
            }
        }

        public List<SubSubCategoryMaster> GetAllSubSubCategoryWithSubCategory()
        {

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                List<SubSubCategoryMaster> subSubCategoryMasters = new List<SubSubCategoryMaster>();
                subSubCategoryMasters = (from subSubCat in dbContext.SubSubCategoryMasters
                                      select subSubCat).ToList();
                foreach (var item in subSubCategoryMasters)
                {
                    item.SubCategoryName = (from subCat in dbContext.SubCategoryMasters where subCat.SubCategoryId == item.SubCategoryId select subCat.SubCategoryName).SingleOrDefault();
                }
                return subSubCategoryMasters;
            }
        }

        public List<SubCategoryMaster> GetAllSubCategory()
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                List<SubCategoryMaster> subCategoryMasters = new List<SubCategoryMaster>();
                subCategoryMasters = (from subCat in dbContext.SubCategoryMasters select subCat).ToList();
                return subCategoryMasters;
            }
        }
        public SubSubCategoryMaster GetSubSubCategoryById(int subSubCategoryId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                SubSubCategoryMaster subSubCategoryMasters = new SubSubCategoryMaster();
                subSubCategoryMasters = (from subSubCat in dbContext.SubSubCategoryMasters where subSubCat.SubSubCategoryId == subSubCategoryId select subSubCat).FirstOrDefault();
                return subSubCategoryMasters;
            }
        }

        public List<KnowledgeCenter> GetAllKnowledgeCenter()
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                List<KnowledgeCenter> knowledgeCenters = new List<KnowledgeCenter>();
                knowledgeCenters = (from knowledgeCenter in dbContext.KnowledgeCenters select knowledgeCenter).ToList();
                return knowledgeCenters;
            }
        }
        public KnowledgeCenter GetKnowledgeById(int Id)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                KnowledgeCenter knowledgeCenter  = new KnowledgeCenter();
                knowledgeCenter = (from knowledge in dbContext.KnowledgeCenters where knowledge.KnowledgeId == Id select knowledge).SingleOrDefault();
                return knowledgeCenter;
            }
        }

        public List<User> GetAllUsers()
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                List<User> users = new List<User>();
                users = (from user in dbContext.Users select user).ToList();
                return users;
            }
        }
        public User GetUserById(int Id)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                User user = new User();
                user = (from usr in dbContext.Users where usr.UserId == Id && usr.IsActive==true select usr).SingleOrDefault();
                return user;
            }
        }
        public User GetUserByUserName(string userName)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                User user = new User();
                user = (from usr in dbContext.Users where usr.Username == userName select usr).SingleOrDefault();
                return user;
            }
        }
    }
}