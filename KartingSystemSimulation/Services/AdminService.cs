using AutoMapper;
using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Models;
using KartingSystemSimulation.Repositories;

namespace KartingSystemSimulation.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public AdminService(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository; // Inject repository
            _mapper = mapper; // Inject AutoMapper
        }

        public IEnumerable<AdminOutputDTO> GetAll()
        {
            var admins = _adminRepository.GetAllAdmins(); // Retrieve all admins
            return _mapper.Map<IEnumerable<AdminOutputDTO>>(admins); // Map to output DTOs
        }

        public AdminOutputDTO GetById(int adminId)
        {
            var admin = _adminRepository.GetAdminById(adminId);
            if (admin == null)
                throw new KeyNotFoundException("Admin not found."); // Handle error

            return _mapper.Map<AdminOutputDTO>(admin); // Map to output DTO
        }

        public void Add(AdminInputDTO adminInput)
        {
            // Map input DTO to entity
            var admin = _mapper.Map<Admin>(adminInput);
            _adminRepository.AddAdmin(admin); // Save admin to database
        }

        public void Update(int adminId, AdminInputDTO adminInput)
        {
            var existingAdmin = _adminRepository.GetAdminById(adminId);
            if (existingAdmin == null)
                throw new KeyNotFoundException("Admin not found."); // Handle error

            // Map updated data to existing admin entity
            _mapper.Map(adminInput, existingAdmin);
            _adminRepository.UpdateAdmin(existingAdmin); // Save changes
        }

        public void Delete(int adminId)
        {
            var admin = _adminRepository.GetAdminById(adminId);
            if (admin == null)
                throw new KeyNotFoundException("Admin not found."); // Handle error

            _adminRepository.DeleteAdmin(admin); // Delete admin
        }
    }
}
