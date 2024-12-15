using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsOrderApp.DTOs;
using SportsOrderApp.Entities;
using SportsOrderApp.Models;
using SportsOrderApp.Repositories;
using static SportsOrderApp.Repositories.IRepositories;

namespace SportsOrderApp.Services
{
    public interface IMockupService
    {
        public void AddNewMockup(AddNewMockup addNewMockup,long max_val);
        public IList<MockupModel> GetMockupsClientRequest();
        public IList<MockupModel> MoveToDesigningDepartment();
        public IList<MockupModel> getMockupsAssignToDesigner();
        public IList<ApprovedMockupModel> GetApprovedMockups(long userId);
        public Task<long> GenerateNewMaxValue(string tblName, string colName);
        public Task<string> SaveImageAsync(IFormFile file, long? mockupId);
       // Task<AddMockupAttachment> GetImageByIdAsync(int id);

    }
    public class MockupService : IMockupService
    {
        private readonly IMapper _mapper;
        private readonly IMockupRepository _mockupRepository;
        private readonly IMockupLogRepository _mockupLogRepository;
        private readonly IMockupAttachmentRepository _mockupAttachmentRepository;
        private readonly IWebHostEnvironment _environment;

        public MockupService(
            IMapper mapper,
            IMockupRepository mockupRepository,
            IMockupLogRepository mockupLogRepository,
            IMockupAttachmentRepository mockupAttachmentRepository, 
            IWebHostEnvironment environment
            )
        {
            _mapper = mapper;
            _mockupRepository = mockupRepository;
            _mockupLogRepository = mockupLogRepository;
            _mockupAttachmentRepository = mockupAttachmentRepository;
            _environment = environment;
        }
        public async Task<long> GenerateNewMaxValue(string tblName , string colName)
        {
            var maxVal = await _mockupRepository.GetNextMaxValueAsync(tblName,colName);
            return maxVal;
        }

        public void AddNewMockup(AddNewMockup addNewMockup, long max_val)
        {
            JsTblMockup mockup = new JsTblMockup();
            mockup.MainCategoryId = addNewMockup.MainCategoryId;
            mockup.CategoryId = addNewMockup.CategoryId;
            mockup.SubCategoryId = addNewMockup.SubCategoryId;
            mockup.ProductId = addNewMockup.ProductId;
            mockup.TeamName = addNewMockup.TeamName;
            mockup.FrontDescription = addNewMockup.FrontDescription;
            mockup.BackDescription = addNewMockup.BackDescription;
            mockup.OnlyDesc = addNewMockup.AdditionalDetail;

            mockup.Active = true;
            mockup.Deleted = false;
            mockup.Status = "CLIENT SEND";
            mockup.CreatedBy = "AAMIR";
            mockup.ModifiedBy = "AAMIR";
            mockup.CreatedDate = DateTime.Now;
            mockup.ModifiedDate = DateTime.Now;
            mockup.MockupRequestNo = max_val;

            _mockupRepository.Add(mockup);
            _mockupRepository.Commit();
            if (mockup.MockupId > 0)
            {
                var SeqNo = GenerateNewMaxValue("JS_TBL_MOCKUP_LOG", "LOG_SEQ_NO").Result;
                JsTblMockupLog jsTblMockupLog = new JsTblMockupLog();

                jsTblMockupLog.MockupId  = mockup.MockupId;
                jsTblMockupLog.ClientUserId = 2;
                mockup.Status = "CLIENT SEND";
                mockup.CreatedBy = "AAMIR";
                mockup.ModifiedBy = "AAMIR";
                mockup.CreatedDate = DateTime.Now;
                mockup.ModifiedDate = DateTime.Now;
                jsTblMockupLog.MockupRequestNo = max_val;
                jsTblMockupLog.LogSeqNo = SeqNo;

                _mockupLogRepository.Add(jsTblMockupLog);
                _mockupLogRepository.Commit();

            }
            //if (mockup.MockupId > 0)
            //{
            //    JsTblMockupAttachment mockupAttachment = new JsTblMockupAttachment();
            //    if (addNewMockup.FrontImages != null)
            //    {
            //        // Save front images
            //        foreach (var frontImage in addNewMockup.FrontImages)
            //        {
            //            if (string.IsNullOrEmpty(frontImage))
            //            {
            //                // Handle null or empty image strings if necessary
            //                continue;
            //            }

                //            // Convert Base64 string to byte array
                //            byte[] imageBytes;
                //            try
                //            {
                //                imageBytes = Convert.FromBase64String(frontImage);
                //            }
                //            catch (FormatException ex)
                //            {
                //                // Handle the case when the Base64 string is not valid
                //                throw new InvalidOperationException("Invalid Base64 image format", ex);
                //            }

                //            // Check the length after conversion to bytes (in bytes, not characters)
                //            if (imageBytes.Length >= 8000)
                //            {
                //                throw new InvalidOperationException("Image size exceeds the allowed limit.");
                //            }

                //            // Assuming MockupAttachment is a new instance, otherwise instantiate it before this loop
                //            //MockupAttachment mockupAttachment = new MockupAttachment
                //            //{
                //            mockupAttachment.MockupId = mockup.MockupId;
                //            //mockupAttachment.FrontImageData = imageBytes;  // Assign byte array
                //            mockupAttachment.Side = "Front";
                //            //};

                //            // Add and commit to the repository
                //            _mockupAttachmentRepository.Add(mockupAttachment);
                //            _mockupAttachmentRepository.Commit();
                //        }
                //        if (addNewMockup.BackImages != null)
                //        {
                //            // Save front images
                //            foreach (var backImage in addNewMockup.BackImages)
                //            {
                //                mockupAttachment.MockupId = mockup.MockupId;
                //                mockupAttachment.FrontImageData = backImage;
                //                mockupAttachment.Side = "Back";
                //                _mockupAttachmentRepository.Add(mockupAttachment);
                //                _mockupAttachmentRepository.Commit();
                //            }
                //        }

                //    }




                //}
        }

        public IList<MockupModel> GetMockupsClientRequest()
        {
            //var max_val = GenerateNewMaxValue().Result;
            var mockups = _mockupRepository.FindBy(x => x.Active == true && x.Deleted == false &&
                                            x.Status == "CLIENT SEND"
                                            ).ToList();

            return _mapper.Map<List<MockupModel>>(mockups);
        }

        public IList<MockupModel> MoveToDesigningDepartment()
        {
            var mockups = _mockupRepository.FindBy(x => x.Active == true && x.Deleted == false &&
                                            x.Status == "MOVE TO DESIGNING"
                                            ).ToList();

            return _mapper.Map<List<MockupModel>>(mockups);
        }
        public IList<MockupModel> getMockupsAssignToDesigner()
        {
            var mockups = _mockupRepository.FindBy(x => x.Active == true && x.Deleted == false &&
                                            x.Status == "ASSIGN TO DESIGNER"
                                            ).ToList();

            return _mapper.Map<List<MockupModel>>(mockups);
        }

        public IList<ApprovedMockupModel> GetApprovedMockups(long userId)
        {
            var approvedMockup = _mockupRepository.FindBy(x => x.Active == true &&
                                                    x.Deleted == false && x.UserId == userId
                                                    )
                                                   .ToList();

            return _mapper.Map<List<ApprovedMockupModel>>(approvedMockup);
        }

        // Images

        public async Task<string> SaveImageAsync(IFormFile file, long? mockupId)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File cannot be null or empty");

            // Check if WebRootPath is null and set it manually if needed
            if (string.IsNullOrEmpty(_environment.WebRootPath))
            {
                _environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                if (!Directory.Exists(_environment.WebRootPath))
                {
                    Directory.CreateDirectory(_environment.WebRootPath); // Create wwwroot if it doesn't exist
                }
            }

            // Combine the path to the uploads folder inside wwwroot
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }


            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //var image = _mapper.Map<JsTblMockupAttachment>
            // Create and save the ImageModel in the repository
            var image = new JsTblMockupAttachment
            {
                MockupId = mockupId,
                FrontImagePath = "/uploads/" + uniqueFileName,
                //ImagePath = "/uploads/" + uniqueFileName,
                //UploadedDate = DateTime.Now
            };

             _mockupAttachmentRepository.Add(image);
             _mockupAttachmentRepository.Commit();

            return image.FrontImagePath;
        }

        //public async Task<AddMockupAttachment> GetImageByIdAsync(int id)
        //{
        //    return await _mockupAttachmentRepository.GetImageByIdAsync(id);
        //}
    }
}
