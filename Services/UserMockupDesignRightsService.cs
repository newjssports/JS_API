using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SportsOrderApp.DTOs;
using SportsOrderApp.Entities;
using SportsOrderApp.Models;
using SportsOrderApp.Repositories;
using static SportsOrderApp.Repositories.IRepositories;

namespace SportsOrderApp.Services
{
    public interface IUserMockupDesignRightsService
    {
        public IList<MockupDesignStepsNameModel> GetMockupDesignNames();
        public IList<MockupDesignStepsNameModel> GetMockupDesignedNames();
        public IList<MockupDesignStepsModel> GetMockupDesignSteps();
        public IList<MockupDesignStepsUserRightsModel> GetUerMockupDesignActionRights(long userId);
        public bool CreateMockupStep(AddMockupDesignStepsModel model);
        public bool CreateMockupDesigningStep(AddMockupDesignStepsModel model);
        
    }
    public class UserMockupDesignRightsService: IUserMockupDesignRightsService
    {
        private readonly IMapper _mapper;
        private readonly IMockupDesignStepsNameRepository _mockupDesignStepsNameRepository;
        private readonly IMockupDesignStepRepository _mockupDesignStepRepository;
        private readonly IMockupDesignStepUserRightRepository _mockupDesignStepUserRightRepository;
        public UserMockupDesignRightsService(
            IMapper mapper,
            IMockupDesignStepsNameRepository mockupDesignStepsNameRepository,
            IMockupDesignStepRepository mockupDesignStepRepository,
            IMockupDesignStepUserRightRepository mockupDesignStepUserRightRepository
            )
        { 
            _mapper = mapper;
            _mockupDesignStepsNameRepository = mockupDesignStepsNameRepository;
            _mockupDesignStepRepository = mockupDesignStepRepository;
            _mockupDesignStepUserRightRepository = mockupDesignStepUserRightRepository;
        }

        public IList<MockupDesignStepsUserRightsModel> GetUerMockupDesignActionRights(long userId)
        {
            
            var res = _mockupDesignStepUserRightRepository.FindBy(
                          x => x.UserId == userId &&
                          x.Deleted == false &&
                          x.Active == true
                  ).ToList();

            return _mapper.Map<List<MockupDesignStepsUserRightsModel>>(res);
        }
        public IList<MockupDesignStepsNameModel> GetMockupDesignNames()
        {
            var res = _mockupDesignStepsNameRepository.FindBy(
                          x =>
                          x.Deleted == false &&
                          x.Active == true && x.IsMockup == true
                  ).ToList();

            return _mapper.Map<List<MockupDesignStepsNameModel>>(res);

        }
        public IList<MockupDesignStepsNameModel> GetMockupDesignedNames()
        {
            var res = _mockupDesignStepsNameRepository.FindBy(
                          x =>
                          x.Deleted == false &&
                          x.Active == true && x.IsDesgin == true
                  ).ToList();

            return _mapper.Map<List<MockupDesignStepsNameModel>>(res);

        }
        public bool CreateMockupStep(AddMockupDesignStepsModel model)
        {
            var isExist = _mockupDesignStepRepository.FindBy(
                                                    x => 
                                                    x.Active == true &&
                                                    x.IsMockup == true &&
                                                    x.Deleted == false &&
                                                    x.FromMockupDesignStepId == model.FromMockupDesignStepId &&
                                                    x.ToMockupDesignStepId == model.ToMockupDesignStepId                                                    
                                                    ).FirstOrDefault();
            if ( isExist == null)
            {
                var steps = new AddMockupDesignStepsModel
                {
                    FromMockupDesignStepId = model.FromMockupDesignStepId,
                    ToMockupDesignStepId = model.ToMockupDesignStepId,

                };
                var res = _mapper.Map<JsTblMockupDesignStep>(steps);
                res.Active = true;
                res.Deleted = false;
                res.IsMockup = true;
                res.IsDesign = false;
                //res.RoleId = 1;
                res.CreatedBy = "AAMIR";
                res.CreatedDate = DateTime.Now;
                res.ModifiedBy = "AAMIR";
                res.ModifiedDate = DateTime.Now;
                _mockupDesignStepRepository.Add(res);
                _mockupDesignStepRepository.Commit();
                return true;
            }
            else
            {
                return false;
            }
            
    }

        public bool CreateMockupDesigningStep(AddMockupDesignStepsModel model)
        {
            var isExist = _mockupDesignStepRepository.FindBy(
                                                    x =>
                                                    x.Active == true &&
                                                    x.IsDesign == true &&
                                                    x.Deleted == false &&
                                                    x.FromMockupDesignStepId == model.FromMockupDesignStepId &&
                                                    x.ToMockupDesignStepId == model.ToMockupDesignStepId
                                                    ).FirstOrDefault();
            if (isExist == null)
            {
                var steps = new AddMockupDesignStepsModel
                {
                    FromMockupDesignStepId = model.FromMockupDesignStepId,
                    ToMockupDesignStepId = model.ToMockupDesignStepId,

                };
                var res = _mapper.Map<JsTblMockupDesignStep>(steps);
                res.Active = true;
                res.Deleted = false;
                res.IsMockup = false;
                res.IsDesign = true;
                //res.RoleId = 1;
                res.CreatedBy = "AAMIR";
                res.CreatedDate = DateTime.Now;
                res.ModifiedBy = "AAMIR";
                res.ModifiedDate = DateTime.Now;
                _mockupDesignStepRepository.Add(res);
                _mockupDesignStepRepository.Commit();
                return true;
            }
            else
            {
                return false;
            }

        }

        public IList<MockupDesignStepsModel> GetMockupDesignSteps()
        {
            var res = _mockupDesignStepRepository.FindBy(
                          x =>
                          x.Deleted == false &&
                          x.Active == true && x.IsMockup == true
                  ).ToList();

            return _mapper.Map<List<MockupDesignStepsModel>>(res);
        }
    }
}
