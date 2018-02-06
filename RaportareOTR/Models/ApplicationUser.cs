using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using SIGAD.CommonCode.Validation;
using RaportareOTR.Data;
using Microsoft.EntityFrameworkCore;
using SIGAD.Models.AccountViewModels;
using System.Text.RegularExpressions;

namespace RaportareOTR.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        [StringLength(20)]
        public string CIF { get; set; }
        // Extended Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastActiveDate { get; set; }

        public ApplicationUser()
        {
            LastActiveDate = DateTime.Now;
            FirstName = "";
            LastName = "";
        }

        public static async Task<List<ValidationReturn>> Validate(ApplicationDbContext context, RegisterMobileResource registerMobileResource, bool isUpdate)
        {
            List<ValidationReturn> validationReturn = new List<ValidationReturn>();

            if (string.IsNullOrWhiteSpace(registerMobileResource.Username))
            {
                validationReturn.Add(new ValidationReturn
                {
                    FieldName = "username",
                    Message = "Field cannot be empty"
                });
            }
            else
            {
                if (!isUpdate)
                {
                    var userInDb = await context.Users.SingleOrDefaultAsync(c => c.UserName.ToLower() == registerMobileResource.Username.ToLower());

                    if (userInDb != null)
                    {
                        validationReturn.Add(new ValidationReturn
                        {
                            FieldName = "username",
                            Message = "Username already exists!"
                        });
                    }
                }

                if (!registerMobileResource.Password.Any(char.IsLetterOrDigit))
                    validationReturn.Add(new ValidationReturn
                    {
                        FieldName = "username",
                        Message = "Username can only contain letters and numbers!"
                    });
            }

            if (string.IsNullOrWhiteSpace(registerMobileResource.Userclass))
            {
                validationReturn.Add(new ValidationReturn
                {
                    FieldName = "userclass",
                    Message = "Field cannot be empty"
                });
            }
            else
            {
                if (registerMobileResource.Userclass.Contains('|'))
                    validationReturn.Add(new ValidationReturn
                    {
                        FieldName = "userclass",
                        Message = "Pipe (|) character cannot be used!"
                    });
            }

            if (string.IsNullOrWhiteSpace(registerMobileResource.Password))
            {
                validationReturn.Add(new ValidationReturn
                {
                    FieldName = "password",
                    Message = "Field cannot be empty"
                });
            }
            else
            {
                if (registerMobileResource.Password.Contains('|'))
                    validationReturn.Add(new ValidationReturn
                    {
                        FieldName = "password",
                        Message = "Pipe (|) character cannot be used!"
                    });

                if (!registerMobileResource.Password.Any(char.IsDigit))
                    validationReturn.Add(new ValidationReturn
                    {
                        FieldName = "password",
                        Message = "Password must contain at least one number!"
                    });

                if (!registerMobileResource.Password.Any(char.IsLower))
                    validationReturn.Add(new ValidationReturn
                    {
                        FieldName = "password",
                        Message = "Password must contain at least one lower case letter!"
                    });

                if (!registerMobileResource.Password.Any(char.IsUpper))
                    validationReturn.Add(new ValidationReturn
                    {
                        FieldName = "password",
                        Message = "Password must contain at least one upper case letter!"
                    });

                if (!registerMobileResource.Password.Any(char.IsPunctuation) && !registerMobileResource.Password.Any(char.IsSeparator) && !registerMobileResource.Password.Any(char.IsSymbol))
                    validationReturn.Add(new ValidationReturn
                    {
                        FieldName = "password",
                        Message = "Password must contain at least one non-alpha numeric character!"
                    });
            }

            if (string.IsNullOrWhiteSpace(registerMobileResource.ConfirmPassword) || registerMobileResource.ConfirmPassword != registerMobileResource.Password)
            {
                validationReturn.Add(new ValidationReturn
                {
                    FieldName = "passwordConfirmation",
                    Message = "Password confirmation is different than password"
                });
            }

            if (string.IsNullOrWhiteSpace(registerMobileResource.Email))
            {
                validationReturn.Add(new ValidationReturn
                {
                    FieldName = "email",
                    Message = "Field cannot be empty"
                });
            }
            else
            {
                if (!isUpdate)
                {
                    var userInDb = await context.Users.SingleOrDefaultAsync(c => c.Email.ToLower() == registerMobileResource.Email.ToLower());

                    if (userInDb != null)
                    {
                        validationReturn.Add(new ValidationReturn
                        {
                            FieldName = "email",
                            Message = "Email already exists!"
                        });
                    }
                }

                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(registerMobileResource.Email);
                if (!match.Success)
                    validationReturn.Add(new ValidationReturn
                    {
                        FieldName = "email",
                        Message = "Invalid email address!"
                    });
            }
            
            return validationReturn;
        }


        public static async Task<bool> Delete(ApplicationDbContext _context, string id)
        {
            ApplicationUser zeUser = await _context.Users.SingleOrDefaultAsync(c => c.Id == id);
            if (zeUser == null) return false;

            _context.Users.Remove(zeUser);
            await _context.SaveChangesAsync();

            return true;
        }

        public static async Task<string> GetUserIdByName(ApplicationDbContext _context, string username)
        {
            ApplicationUser zeUser = await _context.Users.SingleOrDefaultAsync(c => c.UserName == username);
            if (zeUser == null)
            {
                await _context.Error.AddAsync(new Error() { Description = "Invalid User Name provided", Moment = DateTime.Now });
                await _context.SaveChangesAsync();
                return String.Empty;
            }
            else
            {
                return zeUser.Id;
            }            
        }
    }
}
