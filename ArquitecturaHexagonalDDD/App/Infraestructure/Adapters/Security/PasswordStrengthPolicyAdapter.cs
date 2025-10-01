using System.Text.RegularExpressions;
using ArquitecturaHexagonalDDD.App.Application.Users.Port.Out;

namespace ArquitecturaHexagonalDDD.App.Infraestructure.Adapters.Security;

public class PasswordStrengthPolicyAdapter : IPasswordStrengthPolicyPort
{
    public bool IsStrongPassword(string password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < 8)
            return false;

        // Debe contener al menos una letra minúscula
        if (!Regex.IsMatch(password, @"[a-z]"))
            return false;

        // Debe contener al menos una letra mayúscula
        if (!Regex.IsMatch(password, @"[A-Z]"))
            return false;

        // Debe contener al menos un número
        if (!Regex.IsMatch(password, @"[0-9]"))
            return false;

        // Debe contener al menos un carácter especial
        if (!Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]"))
            return false;

        return true;
    }

    public string GetPasswordStrengthMessage(string password)
    {
        if (string.IsNullOrEmpty(password))
            return "La contraseña no puede estar vacía";

        if (password.Length < 8)
            return "La contraseña debe tener al menos 8 caracteres";

        var messages = new List<string>();

        if (!Regex.IsMatch(password, @"[a-z]"))
            messages.Add("debe contener al menos una letra minúscula");

        if (!Regex.IsMatch(password, @"[A-Z]"))
            messages.Add("debe contener al menos una letra mayúscula");

        if (!Regex.IsMatch(password, @"[0-9]"))
            messages.Add("debe contener al menos un número");

        if (!Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]"))
            messages.Add("debe contener al menos un carácter especial");

        return messages.Count > 0 
            ? $"La contraseña {string.Join(", ", messages)}"
            : "La contraseña cumple con todos los requisitos de seguridad";
    }
}
