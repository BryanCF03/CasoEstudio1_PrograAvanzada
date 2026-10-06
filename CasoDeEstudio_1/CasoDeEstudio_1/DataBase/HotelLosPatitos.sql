/* ============================================================
   CASO PRÁCTICO 1 - HOTEL LOS PATITOS
   Base de datos: CASO_PRACTICO_RESERVACIONES
   Servidor: localhost\SQLEXPRESS
   ============================================================ */

-- ============================================================
-- 1. CREAR BASE DE DATOS
-- ============================================================
IF DB_ID('CASO_PRACTICO_RESERVACIONES') IS NOT NULL
BEGIN
    ALTER DATABASE CASO_PRACTICO_RESERVACIONES SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE CASO_PRACTICO_RESERVACIONES;
END
GO

CREATE DATABASE CASO_PRACTICO_RESERVACIONES;
GO

USE CASO_PRACTICO_RESERVACIONES;
GO

-- ============================================================
-- 2. TABLA HABITACIONES
-- ============================================================
CREATE TABLE HABITACIONES (
    Id                          INT IDENTITY(1,1) NOT NULL,
    CodigoDeHabitacion          VARCHAR(7)    NOT NULL,
    NombreDeHabitacion          VARCHAR(30)   NOT NULL,
    CantidadDeHuespedesPermitidos INT         NOT NULL,
    CantidadDeCamas             INT           NOT NULL,
    CantidadDeBanos             INT           NOT NULL,
    Ubicacion                   VARCHAR(10)   NOT NULL,
    EncargadoDeLimpieza         VARCHAR(100)  NOT NULL,
    TipoDeHabitacion            INT           NOT NULL,
    CostoDeLimpieza             DECIMAL(18,2) NOT NULL,
    CostoDeReserva              DECIMAL(18,2) NOT NULL,
    FechaDeRegistro             DATETIME      NOT NULL,
    FechaDeModificacion         DATETIME      NULL,
    Estado                      BIT           NULL,

    CONSTRAINT PK_HABITACIONES PRIMARY KEY CLUSTERED (Id ASC)
);
GO

-- ============================================================
-- 3. TABLA RESERVACIONES
-- ============================================================
CREATE TABLE RESERVACIONES (
    Id                  INT IDENTITY(1,1) NOT NULL,
    NombreDeLaPersona   VARCHAR(150)  NOT NULL,
    Identificacion      VARCHAR(30)   NOT NULL,
    Telefono            VARCHAR(10)   NOT NULL,
    Correo              VARCHAR(50)   NOT NULL,
    FechaNacimiento     DATETIME      NOT NULL,
    Direccion           VARCHAR(200)  NOT NULL,
    MontoTotal          DECIMAL(18,2) NOT NULL,
    FechaInicioReserva  DATETIME      NOT NULL,
    FechaFinReserva     DATETIME      NOT NULL,
    FechaDeRegistro     DATETIME      NOT NULL,
    IdHabitacion        INT           NOT NULL,

    CONSTRAINT PK_RESERVACIONES PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT FK_RESERVACIONES_HABITACIONES
        FOREIGN KEY (IdHabitacion) REFERENCES HABITACIONES(Id)
);
GO

-- ============================================================
-- 4. DATOS DE PRUEBA - HABITACIONES
-- ============================================================
INSERT INTO HABITACIONES
(CodigoDeHabitacion, NombreDeHabitacion, CantidadDeHuespedesPermitidos,
 CantidadDeCamas, CantidadDeBanos, Ubicacion, EncargadoDeLimpieza,
 TipoDeHabitacion, CostoDeLimpieza, CostoDeReserva,
 FechaDeRegistro, FechaDeModificacion, Estado)
VALUES
('HAB-001', 'Habitación Junior 1',   2, 1, 1, 'Piso 1', 'María López',  1,  5000.00, 35000.00, GETDATE(), NULL, 1),
('HAB-002', 'Habitación Junior 2',   2, 1, 1, 'Piso 1', 'María López',  1,  5000.00, 35000.00, GETDATE(), NULL, 1),
('HAB-003', 'Habitación Superior 1', 3, 2, 1, 'Piso 2', 'Juan Pérez',   2,  8000.00, 55000.00, GETDATE(), NULL, 1),
('HAB-004', 'Habitación Superior 2', 3, 2, 1, 'Piso 2', 'Juan Pérez',   2,  8000.00, 55000.00, GETDATE(), NULL, 1),
('HAB-005', 'Habitación Suite 1',    4, 2, 2, 'Piso 3', 'Ana Ruiz',     3, 12000.00, 90000.00, GETDATE(), NULL, 1),
('HAB-006', 'Habitación Suite 2',    4, 2, 2, 'Piso 3', 'Ana Ruiz',     3, 12000.00, 90000.00, GETDATE(), NULL, 1),
('HAB-007', 'Habitación Junior 3',   2, 1, 1, 'Piso 1', 'Carlos Mora',  1,  5000.00, 35000.00, GETDATE(), NULL, 0);
GO

-- ============================================================
-- 5. DATOS DE PRUEBA - RESERVACIONES
-- ============================================================

-- Reservación 1: Habitación Junior 1 (HAB-001)
-- 2 días * 35000 + 5000 limpieza = 75000
INSERT INTO RESERVACIONES
(NombreDeLaPersona, Identificacion, Telefono, Correo, FechaNacimiento,
 Direccion, MontoTotal, FechaInicioReserva, FechaFinReserva,
 FechaDeRegistro, IdHabitacion)
VALUES
('Carlos Ramírez Solís', '1-1234-5678', '88881111', 'carlos.ramirez@correo.com',
 '1990-05-12', 'San José, Costa Rica', 75000.00,
 '2026-03-01', '2026-03-03', GETDATE(), 1);

-- Reservación 2: Habitación Superior 1 (HAB-003)
-- 3 días * 55000 + 8000 limpieza = 173000
INSERT INTO RESERVACIONES
(NombreDeLaPersona, Identificacion, Telefono, Correo, FechaNacimiento,
 Direccion, MontoTotal, FechaInicioReserva, FechaFinReserva,
 FechaDeRegistro, IdHabitacion)
VALUES
('María Fernández Vega', '2-2345-6789', '88882222', 'maria.fernandez@correo.com',
 '1985-08-22', 'Heredia, Costa Rica', 173000.00,
 '2026-03-05', '2026-03-08', GETDATE(), 3);

-- Reservación 3: Habitación Suite 1 (HAB-005)
-- 4 días * 90000 + 12000 limpieza = 372000
INSERT INTO RESERVACIONES
(NombreDeLaPersona, Identificacion, Telefono, Correo, FechaNacimiento,
 Direccion, MontoTotal, FechaInicioReserva, FechaFinReserva,
 FechaDeRegistro, IdHabitacion)
VALUES
('José Pablo Delgado Mora', '3-3456-7890', '88883333', 'jose.delgado@correo.com',
 '1995-11-30', 'Cartago, Costa Rica', 372000.00,
 '2026-04-10', '2026-04-14', GETDATE(), 5);

-- Reservación 4: Habitación Junior 1 (HAB-001)
-- 1 día * 35000 + 5000 limpieza = 40000
INSERT INTO RESERVACIONES
(NombreDeLaPersona, Identificacion, Telefono, Correo, FechaNacimiento,
 Direccion, MontoTotal, FechaInicioReserva, FechaFinReserva,
 FechaDeRegistro, IdHabitacion)
VALUES
('Ana Jiménez Rojas', '4-4567-8901', '88884444', 'ana.jimenez@correo.com',
 '2000-02-14', 'Alajuela, Costa Rica', 40000.00,
 '2026-05-01', '2026-05-02', GETDATE(), 1);
GO

-- ============================================================
-- 6. VERIFICACIÓN
-- ============================================================
PRINT '=== HABITACIONES ===';
SELECT * FROM HABITACIONES;

PRINT '=== RESERVACIONES ===';
SELECT * FROM RESERVACIONES;

PRINT '=== RESERVACIONES POR HABITACIÓN ===';
SELECT
    r.Id            AS IdReservacion,
    r.NombreDeLaPersona,
    r.MontoTotal,
    h.CodigoDeHabitacion,
    h.NombreDeHabitacion,
    h.TipoDeHabitacion
FROM RESERVACIONES r
INNER JOIN HABITACIONES h ON h.Id = r.IdHabitacion
ORDER BY r.Id;
GO