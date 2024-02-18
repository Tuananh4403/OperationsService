namespace OperationsService.Utils;

public class CommonUtils{
    public static string GenerateCode(string roleID, int length, int curNumber)
        {
            string formatStr = $"{{0:D{length}}}"; // e.g., "{0:D7}"
            return roleID + string.Format(formatStr, curNumber);
        }
}