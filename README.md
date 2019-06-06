# Cotizacion
### .net core 
## Configuracion de la aplicacion
en el archivo 
Cotizacion/Cotizacion/appsettings.json
 se deben configurar las secciones:
```
 "MongoConnectionStrings": {
    "CotizacionDB": "mongodb://localhost:27017",
    "Database": "Cotizacion"
  },
  ```
  
  ```
  "SQLConnectionStrings": {
    "ConnectionStringAdmin": "Server=34.241.22.59;Database=Cotizacion;User Id=sa;Password=SaludVida.2018;MultipleActiveResultSets=true"
  },
```
## Creacion Mongo
```
use Cotizacion
```
```
db.createCollection('Users')
```
```
db.Users.insertMany([ 	{ nombre:"Felipe", apellido:"Cardenas", email:"link_the_hero@hotmail.com", password:"8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92"}, 	{ nombre:"Pepe", apellido:"Perez", email:"link_the_hero@hotmail.com", password:"8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92"}, 	{ nombre:"Aurelio", apellido:"Rendon", email:"link_the_hero@hotmail.com", password:"8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92"}, 	{ nombre:"Juana", apellido:"Casallas", email:"link_the_hero@hotmail.com", password:"8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92"}, 	{ nombre:"Ana", apellido:"Bernal", email:"link_the_hero@hotmail.com", password:"8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92"} ])
```




## Creacion SQL
```
CREATE DATABASE Cotizacion;

USE Cotizacion;

DROP TABLE  USUARIO;
create table USUARIO(
	id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	nombre varchar(40) NOT NULL,
	apellido varchar(40) NOT NULL,
	email varchar(255) NOT NULL,
	pass varchar(100) NOT NULL,
);

-- ------------------------------------
-- Usuario statements
-- ------------------------------------
DROP PROCEDURE create_usuario;
GO
CREATE PROCEDURE create_usuario
(
	 @_nombre varchar(40), 
	 @_apellido varchar(40), 
	 @_email varchar(255), 
	 @_pass varchar(100)
)
AS
BEGIN
	INSERT INTO usuario
	(
		nombre, 
		apellido, 
		email, 
		pass
	)
	VALUES
	(
		@_nombre, 
		@_apellido, 
		@_email, 
		@_pass
	);
	
	SELECT @@IDENTITY;
END
GO

DROP PROCEDURE read_usuario;
GO
CREATE PROCEDURE read_usuario
(
	 @_id int
)
AS
BEGIN
	SELECT id, nombre, apellido, email, pass
	FROM usuario
	WHERE id = @_id;
END
GO

DROP PROCEDURE readall_usuario;
GO
CREATE PROCEDURE readall_usuario
AS
BEGIN
	SELECT id, nombre, apellido, email, pass
	FROM usuario;
END
GO

DROP PROCEDURE update_usuario;
GO
CREATE PROCEDURE update_usuario
(
	 @_id int = NULL, 
	 @_nombre varchar(40) = NULL, 
	 @_apellido varchar(40) = NULL, 
	 @_email varchar(255) = NULL, 
	 @_pass varchar(100) = NULL
)
AS
BEGIN
	UPDATE usuario SET 
		nombre = CASE WHEN @_nombre IS NULL THEN nombre ELSE @_nombre END, 
		apellido = CASE WHEN @_apellido IS NULL THEN apellido ELSE @_apellido END, 
		email = CASE WHEN @_email IS NULL THEN email ELSE @_email END, 
		pass = CASE WHEN @_pass IS NULL THEN pass ELSE @_pass END
	WHERE id = @_id;
END
GO

DROP PROCEDURE delete_usuario;
GO
CREATE PROCEDURE delete_usuario
(
	 @_id int
)
AS
BEGIN
	DELETE FROM usuario
	WHERE id = @_id;
END
GO

-- Indexes
DROP PROCEDURE PkUsuario;
GO
CREATE PROCEDURE PkUsuario
(
	@_id int
)
AS
BEGIN
	SELECT id, nombre, apellido, email, pass
	FROM usuario
	WHERE id = @_id;
END
GO

INSERT INTO usuario
	(
		nombre, 
		apellido, 
		email, 
		pass
	)
	VALUES 
	('Felipe','Cardenas','link_the_hero@hotmail.com','8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'	),
	('Pepe','Perez','link_the_hero@hotmail.com','8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'	),
	('Aurelio','Rendon','link_the_hero@hotmail.com','8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'	),
	('Juana','Casallas','link_the_hero@hotmail.com','8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'	),
	('Ana','Bernal','link_the_hero@hotmail.com','8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92'	)
	
	;
```
