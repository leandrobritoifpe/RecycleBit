using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RecycleBitBackEnd.Util.Filters {

    /// <summary>
    ///     Class responsible for validating a Brazilian CEP (Código de Endereçamento Postal).
    /// </summary>
    public class ValidateZipeCodeAttribute : ValidationAttribute {

        /// <summary>
        ///    inicializa a classe ValidateCepAttribute.
        /// </summary>
        public ValidateZipeCodeAttribute() {
            ErrorMessage = "CEP inválido. Use 8 dígitos (com ou sem máscara): NNNNNNNN, NNNNN-NNN ou NN.NNN-NNN.";
        }

        /// <summary>
        ///     Valida o CEP informado considerando os formatos aceitos.
        /// </summary>
        /// <param name="value">Valor do CEP</param>
        /// <returns>true se válido; caso contrário, false.</returns>
        public override bool IsValid(object value) {
            if (value == null)
                return false;

            string cep = value.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(cep))
                return false;

            string[] patterns = new[] {
                @"^\d{8}$",
                @"^\d{5}-\d{3}$",
                @"^\d{2}\.\d{3}-\d{3}$"
            };

            foreach (var pattern in patterns) {
                if (Regex.IsMatch(cep, pattern))
                    return true;
            }

            string onlyDigits = Regex.Replace(cep, @"\D", "");
            return onlyDigits.Length == 8;
        }

        public static string Normalize(string value) {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            string digits = Regex.Replace(value.Trim(), @"\D", "");
            return digits.Length == 8 ? digits : null;
        }
    }
}