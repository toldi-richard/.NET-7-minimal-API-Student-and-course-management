namespace StudentEnrollment.API.Services.IServices;

public interface IFileUpload
{
    string UploadStudentFile(byte[] file, string imageName);
}
