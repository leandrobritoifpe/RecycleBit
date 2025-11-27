using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace RecycleBitBackEnd.Util.Filters {

    /// <summary>
    ///     Class responsible for validating a CPF according to security requirements
    /// </summary>
    public class ValidateCPFAttribute : ValidationAttribute {

        /// <summary>
        ///    Method responsible for initializing the ValidateCPFAttribute class
        /// </summary>
        public ValidateCPFAttribute() {
            ErrorMessage = "O campo CPF deve conter 11 caracteres, devidamente preechido";
        }

        /// <summary>
        ///     Method responsible for validating a cpf
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value) {
            string cpf = (string)value;
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = Regex.Replace(cpf, "[^0-9]", "");

            if (cpf.Length != 11)
                return false;

            if (cpf.All(c => c == cpf[0]))
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCpf += digito1;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            string digitalCalc = digito1.ToString() + digito2.ToString();

            return cpf.EndsWith(digitalCalc);
        }
    }
}