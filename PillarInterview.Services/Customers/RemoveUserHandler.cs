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

        public void Execute(string id)
        {
            var userInfosToDelete = _unitOfWork.UserInfoRepository.Get().Where(e => e.UserId == id).ToList();

            foreach(var userInfo in userInfosToDelete)
            {
                _unitOfWork.UserInfoRepository.Delete(userInfo);
            }

            var user = _unitOfWork.UserRepository.Get().SingleOrDefault(e => e.Id == id);
            _unitOfWork.UserRepository.Delete(user);
        }
    }
}
