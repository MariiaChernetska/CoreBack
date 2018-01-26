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
        /// <summary>
        /// Removes department
        /// </summary>
        /// <param name="id">department id</param>
        public void Execute(int id)
        {
            //get users of the department to delete
            var userInfosToDelete = _unitOfWork.UserInfoRepository.Get(e=>e.User).Where(e => e.DepartmentId == id).ToList();

            foreach(var userInfo in userInfosToDelete)
            {
                var user = userInfo.User;
                _unitOfWork.UserInfoRepository.Delete(userInfo);
                _unitOfWork.UserRepository.Delete(user);
            }
            //remove manager of the department
            var managersToDelete = _unitOfWork.DepartmentManagerRepository.Get().Where(e => e.DepartmentId == id).ToList();

            foreach (var manager in managersToDelete)
            {
                _unitOfWork.DepartmentManagerRepository.Delete(manager);
            }
            //remove department 
            _unitOfWork.DepartmentRepository.Delete(id);
        }
    }
}
