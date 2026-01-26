using FoodCall.Domain.Exceptions;


namespace FoodCall.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public string Description { get; private set; }
        public bool IsActive { get; private set; }
   

    public Category(string name, string description)
    {
        ValidateName(name);
        ValidateDescription(description);

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        IsActive = true;
    }

    public void UpdateName(string name)
    {
        ValidateName(name);
        Name = name;
    }

    public void UpdateDescription(string description)
    {
        ValidateDescription(description);
        Description = description;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    // Validações privadas - regras de negócio da entidade
        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidEntityException("Category", "Nome não pode ser vazio");

            if (name.Length < 2)
                throw new InvalidEntityException("Category", "Nome deve ter pelo menos 2 caracteres");

            if (name.Length > 50)
                throw new InvalidEntityException("Category", "Nome não pode ter mais de 50 caracteres");
        }

        private void ValidateDescription(string description)
        {
            // Descrição é opcional
            if (string.IsNullOrWhiteSpace(description))
                return;

            if (description.Length > 200)
                throw new InvalidEntityException("Category", "Descrição não pode ter mais de 200 caracteres");
        }
    } 
 }   