using FoodCall.Domain.Exceptions;
using FoodCall.Domain.ValueObjects;

namespace FoodCall.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string PasswordHash { get; private set; }
    public List<Address> Addresses { get; private set; } = new();

    public User(string name, string email, string phone, string passwordHash)
    {
        ValidateName(name);
        ValidateEmail(email);
        ValidatePhone(phone);
        ValidatePasswordHash(passwordHash);

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Phone = phone;
        PasswordHash = passwordHash;
    }

    public void AddAddress(Address address)
    {
        if (address == null)
            throw new InvalidEntityException("Address", "Endereço não pode ser nulo");

        Addresses.Add(address);
    }

    public void UpdateName(string name)
    {
        ValidateName(name);
        Name = name;
    }

    public void UpdatePhone(string phone)
    {
        ValidatePhone(phone);
        Phone = phone;
    }

    public void UpdatePassword(string passwordHash)
    {
        ValidatePasswordHash(passwordHash);
        PasswordHash = passwordHash;
    }

    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidEntityException("User", "Nome não pode ser vazio");

        if (name.Length < 3)
            throw new InvalidEntityException("User", "Nome deve ter no mínimo 3 caracteres");

        if (name.Length > 200)
            throw new InvalidEntityException("User", "Nome deve ter no máximo 200 caracteres");
    }

    private void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new InvalidEntityException("User", "Email não pode ser vazio");

        if (!email.Contains("@"))
            throw new InvalidEntityException("User", "Email inválido");

        if (email.Length > 255)
            throw new InvalidEntityException("User", "Email deve ter no máximo 255 caracteres");
    }

    private void ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new InvalidEntityException("User", "Telefone não pode ser vazio");

        if (phone.Length < 10 || phone.Length > 20)
            throw new InvalidEntityException("User", "Telefone deve ter entre 10 e 20 caracteres");
    }

    private void ValidatePasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new InvalidEntityException("User", "Hash da senha não pode ser vazio");
    }
}
