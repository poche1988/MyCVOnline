-- First migrate create profile, add profileId to jobs, qualifications and sections and then update DB

--Get summary, profession and user_id from users to create a profile

--INSERT INTO UserProfiles (Summary, Profession, UserId)
--SELECT Summary, Profession, id FROM AspNetUsers;


--Asign the created profile's id to Jobs, qualifications and sections

--MERGE INTO Jobs 
--   USING UserProfiles
--      ON Jobs.User_Id = UserProfiles.UserId
         
--WHEN MATCHED THEN
--   UPDATE 
--      SET ProfileId = UserProfiles.UserProfileId;

--MERGE INTO Qualifications 
--   USING UserProfiles
--      ON Qualifications.UserId = UserProfiles.UserId
         
--WHEN MATCHED THEN
--   UPDATE 
--      SET ProfileId = UserProfiles.UserProfileId;

--MERGE INTO Sections 
--   USING UserProfiles
--      ON Sections.UserId = UserProfiles.UserId
         
--WHEN MATCHED THEN
--   UPDATE 
--      SET ProfileId = UserProfiles.UserProfileId;




