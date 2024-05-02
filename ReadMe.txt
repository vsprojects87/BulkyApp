
- This is a repository pattern

- we have simply created class library as new project outside our BulkyWeb Project in same solution

- we need to add nuget packages for whole project

- we have to change the napespaces and import names according to our new folders of class library

- we have to add reference of all class library to our main BulkyWeb 

- we are simply doing what we used to do in controller directly now we do it by using functions like
save,delete,removerange,get,getall which is userdefine and we are creating interface where we have this 
functions and we are implementing interfaces in repository and category repository classes 

- we are injecting categoryrepository in controller and we will use categoryrepository rather than dbcontext
since we have implmented and define all the functionality of db operations in category

- we will register categoryrepository in program.cs

- class name and interface must be public so we can access them in controller

- we have created new repository name unitofwork where we have save method implemented and we are accessing 
previous category repository by creating instance of category, at the place of category now we will access the 
all repository using unitofwork repository class,
advantage of these is we have access to all the repository and its cleaner way of writing
disadvantage is that we will be implementing all the repository even when we dont need them since we are calling
unitofwork directly

- now moving on we have created areas for admin and customer which helps to seperate our code
on Bulkyweb project -> Add -> new scaffolded item -> mvc area

- while updating or adding migration we must keep Default project option to our data access repository 
- we will move controller of home to customer and category to admin, we will also move views , we have deleted 
data and model folder since we have already define them above

- we will need to copy viewimport and viewstart in views of both area since it define imports
