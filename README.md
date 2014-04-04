This solution demonstrates my implementation of the repository pattern, in C#.
In this implementation, I am using Entity Framework code-first approach,
LocalDb, Linq and Fluent Validation. I also keep inversion of control in mind
throughout the solution. I also use a service layer as well as a unit of work
object. The ASP.NET MVC4 project uses Ninject for dependency injection.

The ASP.NET MVC4 project demonstrates usage of view models, inversion of
control, dependency injection, validation via DataAnnotations and finally
a basic method of localization.