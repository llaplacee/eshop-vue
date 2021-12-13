using System.Collections.Generic;
using Core.DTO.Paging;

namespace Core.DTO.User
{
    public class GetUsersByFilter : BasePaging
    {
        public string UserName { get; set; }

        public List<DataLayer.Entities.User> Users { get; set; }

        public GetUsersByFilter SetPaging(BasePaging paging)
        {
            PageId = paging.PageId;

            SkipEntity = paging.SkipEntity;

            TakeEntity = paging.TakeEntity;

            ActivePage = paging.PageId;

            EndPage = paging.EndPage;

            PageCount = paging.PageCount;

            StartPage = paging.StartPage;

            return this;
        }

        public GetUsersByFilter SetUsers(List<DataLayer.Entities.User> users)
        {
            Users = users;

            return this;
        }
    }
}
