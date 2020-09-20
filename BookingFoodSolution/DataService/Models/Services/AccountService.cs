using AutoMapper;
using DataService.Enum;
using DataService.Models.RequestModels;
using DataService.Models.RequestModels.PostRequests;
using DataService.Models.RequestModels.PutRequests;
using DataService.Models.Security;
using DataService.Models.ServiceModels;
using DataService.Models.UnitOfWorks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Providers.Entities;

namespace DataService.Models.Services
{

    public interface IAccountService
    {
        AccountServiceModel CheckLogin(LoginRequestModel model);
        Account CreateAccount(AccountPostRequestModel model);
        Account ChangePassword(ChangePasswordRequestModel model);
        IEnumerable<Account> GetAllAccount(int currentId);
        Account Get(int id);
        bool DeleteAccount(int id);
        Account Update(AccountPutRequestModel model);

    }
    public class AccountService : BaseUnitOfWork<UnitOfWork>, IAccountService
    {
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        public AccountService(UnitOfWork uow, IMapper mapper, IOptions<AppSettings> appSettings) : base(uow)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        public AccountServiceModel CheckLogin(LoginRequestModel model)
        {
            // end code MD5 to check login
            var passwordMD5 = EncryptorService.MD5Hash(model.Password);
            var account = _uow.Account.CheckLogin(model.Username, passwordMD5);
            if (account == null)
            {
                return null;
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                // create secret key
                //string secretKey = "This is a secret key to vetify login method of booking food project";
                string secretKey = _appSettings.Secret;
                var key = Encoding.ASCII.GetBytes(secretKey);
                // create token
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                                new Claim(ClaimTypes.Name,account.Id.ToString()),
                                new Claim(ClaimTypes.Role,account.RoleId.ToString())
                        }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                var result = _mapper.Map<Account, AccountServiceModel>(account);
                result.Token = tokenHandler.WriteToken(token);
                return result;
            }


        }
        public Account CreateAccount(AccountPostRequestModel model)
        {
            // if username existed => return null
            var checkUsername = _uow.Account.CheckUsername(model.Username);
            if (checkUsername != null)
            {
                return null;
            }
            else
            {
                Account account = new Account
                {
                    FirstName = model.FirstName,
                    Username = model.Username,
                    Password = EncryptorService.MD5Hash(model.Password),
                    RoleId = (int)RoleEnum.User,
                    IsDelete = 0
                };
                        
                _uow.Account.Create(account);
                // tạo xong thì mới có accountId để tạo ví tiền
                Wallet wallet = new Wallet
                {
                    AccountId = account.Id,
                    Balance = 0,
                    IsDelete = 0
                };
                _uow.Wallet.Create(wallet);
                _uow.Commit();
                return _uow.Account.GetAccountById(account.Id);
            }
        }

        public IEnumerable<Account> GetAllAccount(int currentId)
        {
            
            var result = _uow.Account.GetAccount(currentId);
            if (result == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }
        public Account Get(int id)
        {
            //var result = _uow.Account.Get().Where(p => p.Id == id && p.IsDelete == 0).FirstOrDefault();
            var result = _uow.Account.GetAccountById(id);
            if (result == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }
        public Account ChangePassword(ChangePasswordRequestModel model)
        {
            // check user exist or not
            var user = _uow.Account.GetAccountById(model.Id);
            if (user == null)
            {
                return null;
            }
            else
            {
                var oldPasswordMD5 = EncryptorService.MD5Hash(model.OldPassword);
                // check input password ==? oldpassword
                if (user.Password == oldPasswordMD5)
                {
                    if (model.NewPassword != model.OldPassword)
                    {
                        // hash new password to md5
                        model.NewPassword = EncryptorService.MD5Hash(model.NewPassword);
                        user.Password = model.NewPassword;
                        _uow.Account.Update(user);
                        _uow.Commit();
                        return user;
                    }
                }
                return null;
            }
        }

        public bool DeleteAccount(int id)
        {
            var user = _uow.Account.GetAccountById(id);
            if (user == null)
            {
                return false;
            }
            else
            {
                user.IsDelete = 1;
                _uow.Account.Update(user);
                _uow.Commit();
                return true;
            }
        }
        public bool UnBlockAccount(int id)
        {
            var user = _uow.Account.GetById(id);
            if (user == null)
            {
                return false;
            }
            else
            {
                user.IsDelete = 0;
                _uow.Account.Update(user);
                _uow.Commit();
                return true;
            }
        }

        public Account Update(AccountPutRequestModel model)
        {
            var user = _uow.Account.GetAccountById(model.Id);
            if (user == null)
            {
                return null;
            }
            else
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Address = model.Address;
                user.SexId = model.SexId;
                user.PhoneNumber = model.PhoneNumber;
                user.Gmail = model.Gmail;
                user.Birthday = model.Birthday;
                user.UrlImage = model.UrlImage;
                _uow.Account.Update(user);
                _uow.Commit();
                return _uow.Account.GetAccountById(user.Id);
            }
        }
    }
}
