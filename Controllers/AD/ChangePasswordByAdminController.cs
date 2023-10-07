using Microsoft.AspNetCore.Mvc;
using Novell.Directory.Ldap;
using System.Text;
using System.Security.Cryptography;
using ActiveDirectoryManagement_API.Data;
using ActiveDirectoryManagement_API.Models.Domain.Document;

namespace ActiveDirectoryManagement_API.Controllers.AD
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangePasswordByAdminController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public ChangePasswordByAdminController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        string ldapHost = "localhost";
        int ldapPort = 10389;
        string loginDN = "uid=admin,ou=system";
        string password = "secret";
        string searchBase = "o=MFEC";
        string searchFilter = "(objectClass=inetOrgPerson)";

        [HttpGet]
        public List<LdapEntry> GetCompanyData()
        {

            List<LdapEntry> companyData = new List<LdapEntry>();
            LdapConnection conn = null;

            try
            {
                conn = new LdapConnection();
                conn.Connect(ldapHost, ldapPort);
                conn.Bind(loginDN, password);

                string[] requi = { "cn", "sn", "employeeNumber", "userPassword" };

                LdapSearchResults searchResults = (LdapSearchResults)conn.Search(
                    searchBase,
                    LdapConnection.ScopeSub,
                    searchFilter,
                    requi,
                    false
                );

                while (searchResults.HasMore())
                {
                    LdapEntry entry = null;
                    try
                    {
                        entry = searchResults.Next();
                        companyData.Add(entry);
                    }
                    catch (LdapException e)
                    {
                        Console.WriteLine("LDAP Exception: " + e.Message);
                    }

                    //LdapAttributeSet entryAtt = entry.GetAttributeSet();

                }
                return companyData;
            }
            catch (LdapException e)
            {
                Console.WriteLine("LDAP Exception: " + e.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Disconnect();
                }
            }
        }

        [HttpGet("changepassword")]
        public IActionResult ChangePassword(int documentId)
        {
            LdapConnection conn = null;
            string userId;
            string newPassword;

            if (dbContext.DocumentPasswords.Where(x => x.PasswordId == documentId).FirstOrDefault() is DocumentPassword document)
            {
                userId = document.EmpCode;
                newPassword = document.Password;
            }
            else
            {
                return null;
            }


            try
            {
                conn = new LdapConnection();
                conn.Connect(ldapHost, ldapPort);
                conn.Bind(loginDN, password);

                string userDN = FindUserDN(conn, userId);

                if (userDN != null)
                {
                    // Hash the new password using SHA-256
                    //byte[] newPasswordBytes = Encoding.UTF8.GetBytes(newPassword);
                    //using (SHA256 sha256 = SHA256.Create())
                    //{
                    //    byte[] hashedPasswordBytes = sha256.ComputeHash(newPasswordBytes);
                    //    string hashedPassword = "{SHA256}" + Convert.ToBase64String(hashedPasswordBytes);

                    //    //string passwordHex = ConvertToHex("{SHA256}" + hashedPassword);

                    //    // Modify the "userPassword" attribute with the new hashed password
                    //    LdapAttribute newPasswordAttribute = new LdapAttribute("userPassword", hashedPassword);
                    //    LdapModification passwordChange = new LdapModification(LdapModification.Replace, newPasswordAttribute);
                    //    conn.Modify(userDN, passwordChange);
                    //    return Ok("Password changed successfully.");
                    //}

                    LdapAttribute newPasswordAttribute = new LdapAttribute("userPassword", newPassword);
                    LdapModification passwordChange = new LdapModification(LdapModification.Replace, newPasswordAttribute);
                    conn.Modify(userDN,passwordChange);

                    //return Ok("Password changed successfully.");
                    return Ok(new { message = "Password changed successfully" });


                }
                else
                {
                    return Ok(new { message = "NotFound" });
                }
            }
            catch (LdapException e)
            {
                Console.WriteLine("LDAP Exception: " + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "LDAP error");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Disconnect();
                }
            }
        }

        private string FindUserDN(LdapConnection conn, string userId)
        {
            string[] requi = { "cn", "sn", "employeeNumber", "userPassword" };

            string searchFilter = $"(&(objectClass=inetOrgPerson)(employeeNumber={userId}))";
            LdapSearchResults searchResults = (LdapSearchResults)conn.Search(searchBase, LdapConnection.ScopeSub, searchFilter, requi, false);

            if (searchResults.HasMore())
            {
                LdapEntry entry = searchResults.Next();
                return entry.Dn;
            }

            return null;
        }
    }
}
