using PillarInterview.Data.Repositories;
using System.Linq;

namespace PillarInterview.Services.Customers
{
    public class RemoveUserHandler
    {
        private IUnitOfWork _unitOfWork;

        public RemoveUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Removes user info
        /// </summary>
        /// <param name="id">user id</param>
        public void Execute(string id)
        {
            // get user info of the user
            var userInfosToDelete = _unitOfWork.UserInfoRepository.Get().Where(e => e.UserId == id).ToList();
            //remove each item of user info
            foreach(var userInfo in userInfosToDelete)
            {
                _unitOfWork.UserInfoRepository.Delete(userInfo);
            }
            //find user by id
            var user = _unitOfWork.UserRepository.Get().SingleOrDefault(e => e.Id == id);
            //remove user
            _unitOfWork.UserRepository.Delete(user);
        }
    }
}
