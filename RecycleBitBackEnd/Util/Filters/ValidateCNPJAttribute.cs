using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace RecycleBitBackEnd.Util.Filters {

    /// <summary>
    ///     Classe responsável por validar um CNPJ conforme os requisitos de segurança.
    /// </summary>
    public class ValidateCNPJAttribute : ValidationAttribute {

        /// <summary>
        ///     Método responsável por inicializar a classe ValidateCNPJAttribute.
        /// </summary>
        public ValidateCNPJAttribute() {
            ErrorMessage = "O campo CNPJ deve conter 14 caracteres numéricos, devidamente preenchido";
        }

        /// <summary>
        ///     Método responsável por validar um CNPJ.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value) {
            var input = value as string;
            if (string.IsNullOrWhiteSpace(input))
                return false;

            // Remove qualquer caractere que não seja dígito
            string cnpj = Regex.Replace(input, "[^0-9]", "");

            // CNPJ deve ter 14 dígitos
            if (cnpj.Length != 14)
                return false;

            // Rejeita sequências de dígitos iguais (ex.: 00...0, 11...1, etc.)
            if (cnpj.All(c => c == cnpj[0]))
                return false;

            // Cálculo dos dígitos verificadores
            // Primeiro dígito verificador: usa os 12 primeiros dígitos com pesos 5..2, depois 9..2
            int[] pesosPrimeiro = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] pesosSegundo = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string base12 = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < pesosPrimeiro.Length; i++)
                soma += (base12[i] - '0') * pesosPrimeiro[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            // Segundo dígito verificador: usa os 12 dígitos + primeiro DV com pesos 6..2, depois 9..2
            string base13 = base12 + digito1.ToString();
            soma = 0;

            for (int i = 0; i < pesosSegundo.Length; i++)
                soma += (base13[i] - '0') * pesosSegundo[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            // Verifica se os dois dígitos calculados batem com o CNPJ informado
            string dvCalculado = digito1.ToString() + digito2.ToString();
            return cnpj.EndsWith(dvCalculado);
        }
    }
}