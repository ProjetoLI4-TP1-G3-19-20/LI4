using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LI4 {
    class JWT {

        private string key = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
        public JWT() {
        }

        public string generateToken(int id, bool admin) {
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.key));

            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials
                             (securityKey, SecurityAlgorithms.HmacSha256Signature);

            var header = new JwtHeader(credentials);

            //Some PayLoad that contain information about the  customer
            var payload = new JwtPayload
            {
               { "id", id},
               { "admin", admin}
           };

            //
            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            // Token to String so you can use it in your client
            var tokenString = handler.WriteToken(secToken);

            return tokenString;
        }

        public string generateToken(String nome) {
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.key));

            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials
                             (securityKey, SecurityAlgorithms.HmacSha256Signature);

            var header = new JwtHeader(credentials);

            //Some PayLoad that contain information about the  customer
            var payload = new JwtPayload
            {
               { "nome", nome}
           };

            //
            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            // Token to String so you can use it in your client
            var tokenString = handler.WriteToken(secToken);

            return tokenString;
        }

        public bool validateToken(string token, int id, bool admin) {

            var handler = new JwtSecurityTokenHandler();
            SecurityToken st;
            try {
                handler.ValidateToken(token, this.GetValidationParameters(), out st);
            } catch(Exception e) {
                Console.WriteLine(e.ToString());
                return false;
            }

            JwtSecurityToken t = handler.ReadJwtToken(token);

            JwtPayload payload = t.Payload;

            var enumerator = payload.Claims.GetEnumerator();

            enumerator.MoveNext();
            if (enumerator.Current.Value.ToString().CompareTo(id.ToString()) != 0) return false;
            enumerator.MoveNext();
            if(bool.Parse(enumerator.Current.Value) != admin) return false;

            return true;
        }

        public bool validateToken(string token, String nome) {

            var handler = new JwtSecurityTokenHandler();
            SecurityToken st;
            try {
                handler.ValidateToken(token, this.GetValidationParameters(), out st);
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
                return false;
            }

            JwtSecurityToken t = handler.ReadJwtToken(token);

            JwtPayload payload = t.Payload;

            var enumerator = payload.Claims.GetEnumerator();

            enumerator.MoveNext();
            if (enumerator.Current.Value.CompareTo(nome) != 0) return false;

            return true;
        }

        private TokenValidationParameters GetValidationParameters() {
            return new TokenValidationParameters() {
                ValidateLifetime = false,
                ValidateAudience = false, 
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.key)) 
            };
        }



    }
}
