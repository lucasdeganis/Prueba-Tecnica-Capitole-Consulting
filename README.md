# Prueba tecnica Capitole Consulting
Dentro del proyecto, en la carpeta src, se encuentra el fichero docker-compose.yml. Puedes ejecutarlo para levantar una instancia de MongoDB en Docker y generar la imagen del proyecto automáticamente.
Ten en cuenta lo siguiente:
### Base de datos MongoDB
El puerto expuesto es el 27017.El connection string está securizado por defecto.

### URLs de Swagger según entorno
#### Perfil local:
- https://localhost:62906/swagger/index.html
- http://localhost:62907/swagger/index.html
#### Perfil Docker:
- http://localhost:(puerto mapeado al 80)

Nota: No se utiliza HTTPS, ya que requeriría instalar un certificado.
#### Perfil Docker Compose:
- http://localhost:62907/swagger/index.html

### Datos precargados
Al iniciar el proyecto, se ejecuta el proceso DataSeeder.SeedAsync, que precarga las colecciones de flotas, vehículos y clientes.

### Pruebas
El test InfrastructureTest se conecta directamente a la base de datos, por lo que es necesario tener MongoDB levantado y accesible en el puerto 27017 antes de ejecutarlo.
