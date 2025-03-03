create database DBTemp_Cabrera

use DBTemp_Cabrera

--Creacion de tablas
create table c_Empleado(
IdEmpleado int primary key identity,
nombres varchar(50),
apellidos varchar(50),
usuario varchar(50) UNIQUE,
clave varchar(50),
activo bit
)

create table c_Consumidor(
IdConsumidor int primary key identity,
nombreConsumidor varchar(50) not null,
apellidoConsumidor varchar(50) not null,
direccion varchar(50) not null,
correoElectronico varchar(50) not null,
duiConsumidor varchar(50) UNIQUE not null,
activo bit not null
)

create table c_Estado(
IdEstado int primary key identity,
nombreEstado varchar(50)
)

create table t_Reclamo(
IdReclamo int primary key identity,
nombreProveedor varchar(50) not null,
direccionProveedor varchar(50) not null,
detalleReclamo varchar(250) not null,
telefonoProveedor varchar(10),
montoReclamo decimal(18,2),
fechaIngreso datetime default getdate() not null,
fechaRevision datetime default getdate(),
IdEmpleado int references c_Empleado(IdEmpleado),
IdConsumidor int references c_Consumidor(IdConsumidor),
IdEstado int references c_Estado(IdEstado),
activo bit not null
)

create table t_Asesoria(
IdAsesoria int primary key identity,
fechaIngreso datetime default getdate() not null,
motivoAsesoria varchar(500) not null,
respuestaAsesoria varchar(500) not null,
IdReclamo int references t_Reclamo(IdReclamo),
activo bit not null
)

create table t_Aviso(
IdAviso int primary key identity,
fechaIngreso datetime default getdate() not null,
detalleAviso varchar(500) not null,
IdReclamo int references t_Reclamo(IdReclamo),
activo bit not null
)

select *from c_Empleado
select *from c_Consumidor
select *from c_Estado
select *from t_Reclamo
select *from t_Asesoria
select *from t_Aviso

--Agregando registros
insert into c_Estado(nombreEstado) 
values
('Pendiente de Clasificar'), ('Valido como Reclamo'), ('Clasificado como Asesoria'), ('Clasificado como Aviso de Infraccion')

insert into c_Empleado(nombres, apellidos, usuario, clave, activo)
values
('Erick Alexander', 'Cabrera Gonzalez', 'usuario1', 'clave1', 1),
('Jose Jose', 'Perez Gomez', 'usuario2', 'clave2', 1),
('Alonso Manuel', 'Chavez Campos', 'usuario3', 'clave3', 0)

--Comprobacion de restriccion de valores unicos en Campo:Usuario en tabla c_Empleado
insert into c_Empleado(nombres, apellidos, usuario, clave, activo)
values
('Erick Alexander', 'Prueba', 'usuario1', 'clave4', 1)