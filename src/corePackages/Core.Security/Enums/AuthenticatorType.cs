namespace Core.Security.Enums;

public enum AuthenticatorType
{
    // bu kişi ne ile authenticate olacak
    //direk email ve password ile 
    None = 0,
    //sadece email platformu  ile
    Email = 1,
    Otp = 2
}