using PillarInterview.Data.Repositories;
using System.Linq;

namespace PillarInterview.Services.Customers
{
    public class RemoveDepartmentHandler
    {
        private IUnitOfWork _unitOfWork;

        public RemoveDepartmentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Execute(int id)
        {
            var userInfosToDelete = _unitOfWork.UserInfoRepository.Get(e=>e.User).Where(e => e.DepartmentId == id).ToList();

            foreach(var userInfo in userInfosToDelete)
            {
                var user = userInfo.User;
                _unitOfWork.UserInfoRepository.Delete(userInfo);
                _unitOfWork.UserRepository.Delete(user);
            }

            var managersToDelete = _unitOfWork.DepartmentManagerRepository.Get().Where(e => e.DepartmentId == id).ToList();

            foreach (var manager in managersToDelete)
            {
                _unitOfWork.DepartmentManagerRepository.Delete(manager);
            }

            _unitOfWork.DepartmentRepository.Delete(id);
        }
    }
}
