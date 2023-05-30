using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

/// <summary>
/// Represents an interface for managing user entities in the system.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Gets an existing user entity from the system using its identifier.
    /// </summary>
    /// <param name="id">The integer identifier of the user entity to be retrieved.</param>
    /// <returns>A Task containing a nullable user entity.</returns>
    Task<IUser?> GetUserByIdAsync(int id);

    /// <summary>
    /// Adds a new user entity to the system.
    /// </summary>
    /// <param name="user">The user entity to be added.</param>
    /// <returns>A Task containing an integer representing the result of the operation.</returns>
    Task<int> AddUserAsync(IUser user);

    /// <summary>
    /// Gets an existing user entity from the system using its email address.
    /// </summary>
    /// <param name="email">The email address of the user entity to be retrieved.</param>
    /// <returns>A Task containing a nullable user entity.</returns>
    Task<IUser?> GetUserByEmailAsync(string email);

    /// <summary>
    /// Gets all user entities in the system.
    /// </summary>
    /// <returns>A Task containing a list of all user entities in the system.</returns>
    Task<List<IUser>> GetUsersAsync();

    /// <summary>
    /// Updates an existing user entity in the system.
    /// </summary>
    /// <param name="user">The user entity to be updated.</param>
    /// <returns>A Task tailed with the operation complete status.</returns>
    Task UpdateUserAsync(IUser user);

    /// <summary>
    /// Deletes an existing user entity from the system using its identifier.
    /// </summary>
    /// <param name="id">The integer identifier of the user entity to be deleted.</param>
    /// <returns>A Task tailed with the operation complete status.</returns>
    Task DeleteUserAsync(int id);
}
