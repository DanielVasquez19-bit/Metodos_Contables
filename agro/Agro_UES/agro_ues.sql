-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 01-07-2025 a las 11:40:23
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `agro_ues`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `aprobaciones`
--

CREATE TABLE `aprobaciones` (
  `id_aprobacion` int(11) NOT NULL,
  `tipo_proceso` enum('devolucion','ajuste','otro') NOT NULL,
  `descripcion` text DEFAULT NULL,
  `estado` enum('pendiente','aprobado','rechazado') DEFAULT 'pendiente',
  `usuario_id` int(11) NOT NULL,
  `nombre_usuario_aprueba` varchar(100) DEFAULT NULL,
  `fecha_hora` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `aprobaciones`
--

INSERT INTO `aprobaciones` (`id_aprobacion`, `tipo_proceso`, `descripcion`, `estado`, `usuario_id`, `nombre_usuario_aprueba`, `fecha_hora`) VALUES
(2, '', 'Registro de producto: asas, Precio: 12, Stock: 12, Categoría: 1', 'pendiente', 11, 'Juancho', '2025-06-27 20:06:12'),
(3, '', 'Actualización de producto ID 4: Descripción: asasasas, Precio: 12, Stock: 1222, Fecha de vencimiento: 20/99/1231', 'pendiente', 11, 'Juancho', '2025-06-27 21:20:58'),
(4, '', 'Solicitud de nueva categoría: asas', 'pendiente', 11, 'Juancho', '2025-06-28 12:52:18'),
(5, '', 'Registro de producto: asasas, Precio: 12, Stock: 12, Categoría: 2', 'pendiente', 11, 'Juancho', '2025-06-29 13:52:21'),
(6, '', 'Actualización de producto ID 1: Descripción: Fertilizante nitrogenado, Precio: 12, Stock: 123, Fecha de vencimiento: 12/12/1212', 'pendiente', 11, 'Juancho', '2025-06-29 13:53:06'),
(7, '', 'Actualización de producto ID 1: Descripción: Fertilizante nitrogenado, Precio: 12, Stock: 50, Fecha de vencimiento: 12/12/1212', 'pendiente', 11, 'Juancho', '2025-06-29 13:58:32'),
(8, '', 'Actualización de producto ID 1: Descripción: Fertilizante nitrogenado, Precio: 122, Stock: 124, Fecha de vencimiento: /  /', 'pendiente', 11, 'Juancho', '2025-06-29 13:58:47');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `aprobaciones_almacen`
--

CREATE TABLE `aprobaciones_almacen` (
  `id_aprobacion` int(11) NOT NULL,
  `id_producto` int(11) NOT NULL,
  `descripcion` text NOT NULL,
  `precio` decimal(10,2) NOT NULL,
  `stock` int(11) NOT NULL,
  `fecha_vencimiento` date DEFAULT NULL,
  `estado` enum('Pendiente','Aprobada','Rechazada') DEFAULT 'Pendiente',
  `usuario_solicita` int(11) NOT NULL,
  `nombre_solicita` varchar(100) DEFAULT NULL,
  `fecha_solicita` datetime DEFAULT current_timestamp(),
  `usuario_responde` int(11) DEFAULT NULL,
  `nombre_responde` varchar(100) DEFAULT NULL,
  `fecha_respuesta` datetime DEFAULT NULL,
  `observacion` text DEFAULT NULL,
  `ruta_imagen` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `aprobaciones_almacen`
--

INSERT INTO `aprobaciones_almacen` (`id_aprobacion`, `id_producto`, `descripcion`, `precio`, `stock`, `fecha_vencimiento`, `estado`, `usuario_solicita`, `nombre_solicita`, `fecha_solicita`, `usuario_responde`, `nombre_responde`, `fecha_respuesta`, `observacion`, `ruta_imagen`) VALUES
(1, 2, 'Herramienta para labranza', 13.75, 17, '2060-04-12', 'Aprobada', 3, 'Almacen', '2025-06-29 18:59:32', 2, 'Gerente', '2025-06-29 20:35:59', NULL, NULL),
(10, 1, 'Cambio de descripción para producto 1', 4.25, 20, '2025-12-01', 'Rechazada', 3, 'Almacen', '2025-06-29 20:48:05', 2, 'Gerente', '2025-06-30 21:11:10', 'No especificado', NULL),
(11, 2, 'Actualización de precio para producto 2', 9.90, 15, '2025-10-15', 'Pendiente', 3, 'Almacen', '2025-06-29 20:48:05', NULL, NULL, NULL, NULL, NULL),
(12, 3, 'Corrección de fecha de vencimiento', 6.40, 10, '2026-01-01', 'Pendiente', 3, 'Almacen', '2025-06-29 20:48:05', NULL, NULL, NULL, NULL, NULL),
(13, 4, 'Stock corregido después de ajuste físico', 2.80, 35, '2025-11-10', 'Pendiente', 3, 'Almacen', '2025-06-29 20:48:05', NULL, NULL, NULL, NULL, NULL),
(14, 1, 'Reetiquetado y nuevo precio sugerido', 7.50, 5, '2026-03-20', 'Pendiente', 3, 'Almacen', '2025-06-29 20:48:05', NULL, NULL, NULL, NULL, NULL),
(15, 2, 'Modificación por error de carga original', 3.15, 8, '2025-09-05', 'Pendiente', 3, 'Almacen', '2025-06-29 20:48:05', NULL, NULL, NULL, NULL, NULL),
(16, 3, 'Producto con promoción especial', 1.99, 50, '2025-12-31', 'Pendiente', 3, 'Almacen', '2025-06-29 20:48:05', NULL, NULL, NULL, NULL, NULL),
(17, 4, 'Revisión de caducidad y stock por rotación', 5.60, 12, '2026-02-14', 'Pendiente', 3, 'Almacen', '2025-06-29 20:48:05', NULL, NULL, NULL, NULL, NULL),
(18, 1, 'Fertilizante nitrogenado', 25.50, 50, '2025-11-07', 'Aprobada', 13, 'Bodega', '2025-06-30 18:17:23', 12, 'gerente', '2025-06-30 18:18:00', NULL, 'imagenes\\generate an image of.jpg'),
(19, 0, 'semilla de arbol amarillo', 20.00, 5, '2026-03-20', 'Aprobada', 13, 'Bodega', '2025-06-30 18:42:20', 12, 'gerente', '2025-06-30 19:33:25', NULL, 'imagenes\\generate an image of.png'),
(20, 4, 'asasasas', 12.00, 1222, '2025-07-01', 'Pendiente', 3, 'Almacen', '2025-07-01 03:11:05', NULL, NULL, NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `categorias`
--

CREATE TABLE `categorias` (
  `id_categoria` int(11) NOT NULL,
  `nombre_categoria` varchar(100) NOT NULL,
  `estado` enum('Activa','Inactiva') NOT NULL DEFAULT 'Inactiva'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `categorias`
--

INSERT INTO `categorias` (`id_categoria`, `nombre_categoria`, `estado`) VALUES
(1, 'Fertilizantes', 'Activa'),
(2, 'Herramientas', 'Activa'),
(3, 'Semillas', 'Activa'),
(5, 'Azas', 'Activa'),
(6, 'herbicidas', 'Activa');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalle_ventas`
--

CREATE TABLE `detalle_ventas` (
  `id_detalle` int(11) NOT NULL,
  `venta_id` int(11) NOT NULL,
  `producto_id` int(11) NOT NULL,
  `nombre_producto` varchar(100) DEFAULT NULL,
  `cantidad` int(11) NOT NULL,
  `precio_unitario` decimal(10,2) NOT NULL,
  `subtotal` decimal(10,2) GENERATED ALWAYS AS (`cantidad` * `precio_unitario`) STORED
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `detalle_ventas`
--

INSERT INTO `detalle_ventas` (`id_detalle`, `venta_id`, `producto_id`, `nombre_producto`, `cantidad`, `precio_unitario`) VALUES
(1, 1, 2, 'Azadón', 3, 13.75),
(2, 2, 5, 'Girasol', 1, 10.00),
(3, 3, 6, 'veneno par sacate', 1, 15.20),
(4, 4, 5, 'Girasol', 1, 10.00);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `historial_acciones`
--

CREATE TABLE `historial_acciones` (
  `id_historial` int(11) NOT NULL,
  `usuario_id` int(11) NOT NULL,
  `nombre_usuario` varchar(100) DEFAULT NULL,
  `accion` text NOT NULL,
  `fecha_hora` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `historial_acciones`
--

INSERT INTO `historial_acciones` (`id_historial`, `usuario_id`, `nombre_usuario`, `accion`, `fecha_hora`) VALUES
(1, 1, 'Admin', 'Cambio de contraseña', '2025-06-22 18:45:10'),
(2, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-22 18:56:25'),
(3, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 07:23:18'),
(4, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 07:24:25'),
(5, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 07:25:18'),
(6, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 07:32:10'),
(7, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 09:32:09'),
(8, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 09:32:16'),
(9, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 09:32:48'),
(10, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 09:39:26'),
(11, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 09:42:24'),
(12, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 09:47:22'),
(13, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 09:58:06'),
(14, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 09:59:39'),
(15, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 10:29:46'),
(16, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 10:32:02'),
(17, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 10:33:22'),
(18, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 10:47:40'),
(19, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 11:52:59'),
(20, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 12:06:02'),
(21, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 12:07:19'),
(22, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 12:15:23'),
(23, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 13:03:51'),
(24, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 13:17:05'),
(25, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 22:56:22'),
(26, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 23:01:51'),
(27, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 23:08:34'),
(28, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 23:12:34'),
(29, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-24 23:14:40'),
(30, 1, 'Admin', 'Registro nuevo usuario: Josue Carlos', '2025-06-24 23:15:13'),
(31, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 00:49:20'),
(32, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 01:07:01'),
(33, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 01:10:43'),
(34, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 01:11:34'),
(35, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 02:35:45'),
(36, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 02:36:39'),
(37, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 02:42:49'),
(38, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 02:44:15'),
(39, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 02:54:42'),
(40, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 02:56:38'),
(41, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 02:59:24'),
(42, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 03:02:20'),
(43, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 03:02:46'),
(44, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 03:04:57'),
(45, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 03:14:37'),
(46, 1, 'Admin', 'Generó reporte: Productos (18/06/2025 - 25/06/2025)', '2025-06-25 03:14:55'),
(47, 1, 'Admin', 'Generó respaldo de la base de datos', '2025-06-25 03:15:52'),
(48, 1, 'Admin', 'Generó respaldo de la base de datos', '2025-06-25 03:16:50'),
(49, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 03:21:09'),
(50, 1, 'Admin', 'Generó respaldo de la base de datos', '2025-06-25 03:21:11'),
(51, 1, 'Admin', 'Generó respaldo de la base de datos', '2025-06-25 03:21:49'),
(52, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 03:25:29'),
(53, 1, 'Admin', 'Genero respaldo de la base de datos', '2025-06-25 03:25:32'),
(54, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 03:37:26'),
(55, 1, 'Admin', 'Generó respaldo de la base de datos', '2025-06-25 03:38:06'),
(56, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 03:39:18'),
(57, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-25 03:40:19'),
(58, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-26 11:11:09'),
(59, 1, 'Admin', 'Generó respaldo de la base de datos', '2025-06-26 11:12:37'),
(60, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-26 15:38:45'),
(61, 1, 'Admin', 'Registro nuevo usuario: Juan Carlos', '2025-06-26 15:39:32'),
(62, 10, 'Juan Carlos', 'Inicio de sesión en el sistema', '2025-06-26 15:40:39'),
(63, 10, 'Juan Carlos', 'Inicio de sesión en el sistema', '2025-06-27 15:24:00'),
(64, 10, 'Juan Carlos', 'Venta realizada. Total: 46.61', '2025-06-27 15:24:55'),
(65, 10, 'Juan Carlos', 'Inicio de sesión en el sistema', '2025-06-27 15:26:14'),
(66, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-27 15:36:53'),
(67, 1, 'Admin', 'Registro nuevo usuario: Juancho', '2025-06-27 15:37:42'),
(68, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-27 15:37:57'),
(69, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-27 18:33:51'),
(70, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-27 18:37:59'),
(71, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-27 19:57:45'),
(72, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-27 20:01:41'),
(73, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-27 20:04:55'),
(74, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-27 20:06:02'),
(75, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-27 20:59:13'),
(76, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-27 21:00:03'),
(77, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-27 21:04:59'),
(78, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-27 21:06:44'),
(79, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-27 21:08:46'),
(80, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-27 21:09:25'),
(81, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-27 21:12:03'),
(82, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-27 21:16:49'),
(83, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-27 21:20:06'),
(84, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-27 21:20:50'),
(85, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-28 12:22:11'),
(86, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-28 12:24:07'),
(87, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-28 12:26:54'),
(88, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-28 12:52:09'),
(89, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-28 13:18:08'),
(90, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-28 13:18:40'),
(91, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-28 13:19:56'),
(92, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-28 13:20:32'),
(93, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-28 13:22:54'),
(94, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-29 13:52:09'),
(95, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-29 13:52:55'),
(96, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-29 13:55:12'),
(97, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-06-29 13:58:25'),
(98, 3, 'Almacen', 'Inicio de sesión en el sistema', '2025-06-29 18:58:52'),
(99, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-29 19:07:40'),
(100, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-29 19:10:39'),
(101, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-29 19:13:14'),
(102, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-29 19:15:20'),
(103, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-29 19:27:48'),
(104, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-29 19:29:21'),
(105, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-29 19:32:30'),
(106, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-29 19:36:30'),
(107, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-29 19:43:38'),
(108, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-29 19:46:30'),
(109, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-29 19:54:03'),
(110, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-29 20:16:58'),
(111, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-29 20:20:13'),
(112, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-29 20:28:59'),
(113, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-29 20:35:54'),
(114, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 14:25:49'),
(115, 1, 'Admin', 'Registro nuevo usuario: gerente', '2025-06-30 14:26:15'),
(116, 12, 'gerente', 'Inicio de sesión en el sistema', '2025-06-30 14:26:30'),
(117, 12, 'gerente', 'Inicio de sesión en el sistema', '2025-06-30 14:43:33'),
(118, 12, 'gerente', 'Inicio de sesión en el sistema', '2025-06-30 14:44:29'),
(119, 12, 'gerente', 'Inicio de sesión en el sistema', '2025-06-30 14:46:14'),
(120, 12, 'gerente', 'Inicio de sesión en el sistema', '2025-06-30 14:49:45'),
(121, 12, 'gerente', 'Inicio de sesión en el sistema', '2025-06-30 14:51:44'),
(122, 12, 'gerente', 'Inicio de sesión en el sistema', '2025-06-30 14:58:13'),
(123, 12, 'gerente', 'Inicio de sesión en el sistema', '2025-06-30 16:16:22'),
(124, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 16:17:30'),
(125, 1, 'Admin', 'Registro nuevo usuario: Bodega', '2025-06-30 16:18:03'),
(126, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 16:18:37'),
(127, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 16:25:38'),
(128, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 16:31:54'),
(129, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 16:33:04'),
(130, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 16:57:45'),
(131, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 16:59:38'),
(132, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 17:03:24'),
(133, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 17:55:10'),
(134, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 17:57:01'),
(135, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 18:00:40'),
(136, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 18:04:05'),
(137, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 18:11:42'),
(138, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 18:17:11'),
(139, 13, 'Bodega', 'Solicito modificacion del producto: Urea 46%', '2025-06-30 18:17:23'),
(140, 12, 'gerente', 'Inicio de sesión en el sistema', '2025-06-30 18:17:49'),
(141, 12, 'gerente', 'Aprob solicitud #18 – Producto ID: 1', '2025-06-30 18:18:00'),
(142, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 18:36:17'),
(143, 1, 'Admin', 'Registro nuevo usuario: Axel', '2025-06-30 18:37:17'),
(144, 14, 'Axel', 'Inicio de sesión en el sistema', '2025-06-30 18:37:35'),
(145, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 18:39:11'),
(146, 13, 'Bodega', 'Solicito registro de producto: semilla sesamo', '2025-06-30 18:42:20'),
(147, 12, 'gerente', 'Inicio de sesión en el sistema', '2025-06-30 18:43:43'),
(148, 12, 'gerente', 'Inicio de sesión en el sistema', '2025-06-30 18:57:51'),
(149, 12, 'gerente', 'Inicio de sesión en el sistema', '2025-06-30 19:19:53'),
(150, 12, 'gerente', 'Inicio de sesión en el sistema', '2025-06-30 19:33:15'),
(151, 12, 'gerente', '✔️ Aprobó modificación del producto ID 0 (solicitud #19)', '2025-06-30 19:33:25'),
(152, 12, 'gerente', 'Inicio de sesión en el sistema', '2025-06-30 20:00:17'),
(153, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 20:00:54'),
(154, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 20:12:19'),
(155, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 20:14:40'),
(156, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 20:17:00'),
(157, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 20:17:38'),
(158, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 20:18:23'),
(159, 13, 'Bodega (Encargado de Almacen)', 'Activo categoría ID 5', '2025-06-30 20:18:31'),
(160, 13, 'Bodega', 'Inicio de sesión en el sistema', '2025-06-30 20:19:53'),
(161, 13, 'Bodega (Encargado de Almacen)', 'Modifico categoria ID 5 → Azas', '2025-06-30 20:20:24'),
(162, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-30 20:46:22'),
(163, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-30 20:59:16'),
(164, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-30 21:10:57'),
(165, 2, 'Gerente', 'Rechazo solicitud #10 – Motivo: No especificado', '2025-06-30 21:11:12'),
(166, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 21:29:27'),
(167, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 21:40:22'),
(168, 1, 'Admin', 'Genero respaldo de la base de datos', '2025-06-30 21:41:00'),
(169, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 21:53:01'),
(170, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 21:53:33'),
(171, 1, 'Admin', 'Genero respaldo de la base de datos', '2025-06-30 21:53:49'),
(172, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-30 21:55:47'),
(173, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 21:56:08'),
(174, 1, 'Admin', 'Genero respaldo de la base de datos', '2025-06-30 21:56:22'),
(175, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 21:57:16'),
(176, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 22:01:46'),
(177, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 22:02:24'),
(178, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 22:16:29'),
(179, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 22:31:43'),
(180, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 22:33:50'),
(181, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 22:41:55'),
(182, 1, 'Admin', 'Desactivo al usuario: Josue Carlos', '2025-06-30 22:42:09'),
(183, 1, 'Admin', 'Desactivo al usuario: Cajero', '2025-06-30 22:44:51'),
(184, 4, 'Cajero', 'Inicio de sesión en el sistema', '2025-06-30 22:46:28'),
(185, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 22:55:00'),
(186, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 22:56:39'),
(187, 1, 'Admin', 'Reactivo al usuario: Cajero', '2025-06-30 22:56:58'),
(188, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-30 23:04:22'),
(189, 2, 'Gerente', 'Inicio de sesión en el sistema', '2025-06-30 23:21:17'),
(190, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 23:26:33'),
(191, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 23:35:00'),
(192, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-06-30 23:43:17'),
(193, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-07-01 00:33:07'),
(194, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-07-01 01:23:04'),
(195, 11, 'Juancho', 'Registró producto: Girasol', '2025-07-01 01:23:59'),
(196, 11, 'Juancho', 'Inicio de sesión en el sistema', '2025-07-01 01:25:18'),
(197, 4, 'Cajero', 'Inicio de sesión en el sistema', '2025-07-01 01:26:01'),
(198, 4, 'Cajero', 'Inicio de sesión en el sistema', '2025-07-01 01:28:29'),
(199, 4, 'Cajero', 'Inicio de sesión en el sistema', '2025-07-01 01:45:03'),
(200, 3, 'Almacen', 'Inicio de sesión en el sistema', '2025-07-01 01:46:44'),
(201, 4, 'Cajero', 'Inicio de sesión en el sistema', '2025-07-01 02:09:28'),
(202, 4, 'Cajero', 'Inicio de sesión en el sistema', '2025-07-01 02:30:32'),
(203, 4, 'Cajero', 'Inicio de sesión en el sistema', '2025-07-01 02:31:29'),
(204, 4, 'Cajero', 'Inicio de sesión en el sistema', '2025-07-01 02:33:21'),
(205, 4, 'Cajero', 'Inicio de sesión en el sistema', '2025-07-01 02:45:20'),
(206, 4, 'Cajero', 'Venta realizada. Total: 11.30', '2025-07-01 02:46:31'),
(207, 3, 'Almacen', 'Inicio de sesión en el sistema', '2025-07-01 03:08:47'),
(208, 3, 'Almacen (Encargado de Almacen)', 'Agrego categoria: herbicidas', '2025-07-01 03:09:30'),
(209, 3, 'Almacen', 'Registró producto: veneno par sacate', '2025-07-01 03:10:43'),
(210, 3, 'Almacen', 'Solicitó modificación del producto ID 4', '2025-07-01 03:11:05'),
(211, 4, 'Cajero', 'Inicio de sesión en el sistema', '2025-07-01 03:11:24'),
(212, 4, 'Cajero', 'Venta realizada. Total: 17.18', '2025-07-01 03:12:05'),
(213, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-07-01 03:12:21'),
(214, 1, 'Admin', 'Genero reporte: Productos (24/06/2025 - 01/07/2025)', '2025-07-01 03:13:22'),
(215, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-07-01 03:32:07'),
(216, 1, 'Admin', 'Inicio de sesión en el sistema', '2025-07-01 03:33:06'),
(217, 1, 'Admin', 'Venta realizada. Total: 11.30', '2025-07-01 03:33:26');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productos`
--

CREATE TABLE `productos` (
  `id_producto` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `descripcion` text DEFAULT NULL,
  `categoria_id` int(11) NOT NULL,
  `precio` decimal(10,2) NOT NULL,
  `stock` int(11) NOT NULL DEFAULT 0,
  `fecha_vencimiento` date DEFAULT NULL,
  `alerta_bajo_stock` tinyint(1) DEFAULT 0,
  `ruta_imagen` varchar(255) DEFAULT NULL,
  `estado` varchar(20) NOT NULL DEFAULT 'Pendiente'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `productos`
--

INSERT INTO `productos` (`id_producto`, `nombre`, `descripcion`, `categoria_id`, `precio`, `stock`, `fecha_vencimiento`, `alerta_bajo_stock`, `ruta_imagen`, `estado`) VALUES
(1, 'Urea 46%', 'Fertilizante nitrogenado', 1, 25.50, 50, '2025-11-07', 0, NULL, 'Activo'),
(2, 'Azadón', 'Herramienta para labranza', 2, 13.75, 17, '2060-04-12', 1, NULL, 'Activo'),
(3, 'Maíz H-59', 'Semilla híbrida de maíz', 3, 7.90, 100, NULL, 0, NULL, 'Activo'),
(4, 'asas', 'asasasas', 1, 12.00, 1222, NULL, NULL, NULL, 'Activo'),
(5, 'Girasol', 'Semillas girasol', 3, 10.00, 6, '2025-10-10', 0, 'imagenes\\generate an image of.png', 'Disponible'),
(6, 'veneno par sacate', 'mata el pasto', 6, 15.20, 4, '2029-07-14', 0, 'imagenes\\Paraquat-aleman-20-sl-2-480x480.jpg', 'Disponible');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `recuperacion_password`
--

CREATE TABLE `recuperacion_password` (
  `id` int(11) NOT NULL,
  `usuario_id` int(11) NOT NULL,
  `codigo` varchar(10) NOT NULL,
  `fecha_solicitud` datetime NOT NULL,
  `usado` tinyint(1) DEFAULT 0,
  `usuario_modifico` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `recuperacion_password`
--

INSERT INTO `recuperacion_password` (`id`, `usuario_id`, `codigo`, `fecha_solicitud`, `usado`, `usuario_modifico`) VALUES
(1, 1, '995449', '2025-06-22 18:09:40', 0, NULL),
(2, 1, '474219', '2025-06-22 18:43:53', 1, NULL),
(3, 1, '350051', '2025-06-30 21:41:36', 0, NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `respaldos`
--

CREATE TABLE `respaldos` (
  `id_respaldo` int(11) NOT NULL,
  `usuario_id` int(11) NOT NULL,
  `ruta_archivo` varchar(255) NOT NULL,
  `fecha_hora` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `roles`
--

CREATE TABLE `roles` (
  `id_rol` int(11) NOT NULL,
  `nombre_rol` varchar(50) NOT NULL,
  `descripcion` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `roles`
--

INSERT INTO `roles` (`id_rol`, `nombre_rol`, `descripcion`) VALUES
(1, 'Super Administrador', 'Acceso total al sistema'),
(2, 'Gerente', 'Gestión de reportes y aprobaciones'),
(3, 'Cajero', 'Realiza ventas'),
(4, 'Encargado de Almacen', 'Gestiona productos y stock');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `solicitudes_devoluciones`
--

CREATE TABLE `solicitudes_devoluciones` (
  `id_solicitud` int(11) NOT NULL,
  `id_venta` int(11) NOT NULL,
  `id_producto` int(11) NOT NULL,
  `cantidad_devuelta` int(11) NOT NULL,
  `motivo` text NOT NULL,
  `estado` enum('Pendiente','Aprobada','Rechazada') DEFAULT 'Pendiente',
  `usuario_solicita` int(11) NOT NULL,
  `nombre_solicita` varchar(100) DEFAULT NULL,
  `fecha_solicita` datetime DEFAULT current_timestamp(),
  `usuario_responde` int(11) DEFAULT NULL,
  `nombre_responde` varchar(100) DEFAULT NULL,
  `fecha_respuesta` datetime DEFAULT NULL,
  `observacion` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `id_usuario` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `correo` varchar(100) NOT NULL,
  `contraseña_hash` varchar(255) NOT NULL,
  `rol_id` int(11) NOT NULL,
  `fecha_registro` datetime DEFAULT current_timestamp(),
  `estado` enum('activo','inactivo') DEFAULT 'activo',
  `sesion` enum('activa','inactiva') NOT NULL DEFAULT 'inactiva'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`id_usuario`, `nombre`, `correo`, `contraseña_hash`, `rol_id`, `fecha_registro`, `estado`, `sesion`) VALUES
(1, 'Admin', 'hm23052@ues.edu.sv', '41c991eb6a66242c0454191244278183ce58cf4a6bcd372f799e4b9cc01886af', 1, '2025-06-22 15:39:12', 'activo', 'inactiva'),
(2, 'Gerente', 'gerente@agro.com', '41c991eb6a66242c0454191244278183ce58cf4a6bcd372f799e4b9cc01886af', 2, '2025-06-22 15:39:12', 'activo', 'inactiva'),
(3, 'Almacen', 'almacen@agro.com', '41c991eb6a66242c0454191244278183ce58cf4a6bcd372f799e4b9cc01886af', 4, '2025-06-22 15:39:12', 'activo', 'inactiva'),
(4, 'Cajero', 'cajero@agro.com', '41c991eb6a66242c0454191244278183ce58cf4a6bcd372f799e4b9cc01886af', 3, '2025-06-22 15:39:12', 'activo', 'activa'),
(9, 'Josue Carlos', 'Carlos@agro.com', '84b2a5d834daee2fff7eb5e31f44ba68eb860d86d2cf8e37606a26fa775cf23b', 3, '2025-06-24 23:15:13', 'inactivo', 'inactiva'),
(10, 'Juan Carlos', 'dsadas@gmail.com', 'cbfad02f9ed2a8d1e08d8f74f5303e9eb93637d47f82ab6f1c15871cf8dd0481', 3, '2025-06-26 15:39:32', 'activo', 'inactiva'),
(11, 'Juancho', 'aaaaa@gmail.com', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 4, '2025-06-27 15:37:42', 'activo', 'inactiva'),
(12, 'gerente', 'das', '888b19a43b151683c87895f6211d9f8640f97bdc8ef32f03dbe057c8f5e56d32', 2, '2025-06-30 14:26:15', 'activo', 'inactiva'),
(13, 'Bodega', 'bodega@dsa', '4fac6dbe26e823ed6edf999c63fab3507119cf3cbfb56036511aa62e258c35b4', 4, '2025-06-30 16:18:03', 'activo', 'inactiva'),
(14, 'Axel', 'alexmorapat4@gmail.com', 'cbfad02f9ed2a8d1e08d8f74f5303e9eb93637d47f82ab6f1c15871cf8dd0481', 3, '2025-06-30 18:37:17', 'activo', 'inactiva');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ventas`
--

CREATE TABLE `ventas` (
  `id_venta` int(11) NOT NULL,
  `usuario_id` int(11) NOT NULL,
  `fecha_venta` datetime DEFAULT current_timestamp(),
  `total` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `ventas`
--

INSERT INTO `ventas` (`id_venta`, `usuario_id`, `fecha_venta`, `total`) VALUES
(1, 10, '2025-06-27 15:24:55', 46.61),
(2, 4, '2025-07-01 02:46:31', 11.30),
(3, 4, '2025-07-01 03:12:05', 17.18),
(4, 1, '2025-07-01 03:33:26', 11.30);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `aprobaciones`
--
ALTER TABLE `aprobaciones`
  ADD PRIMARY KEY (`id_aprobacion`),
  ADD KEY `usuario_id` (`usuario_id`);

--
-- Indices de la tabla `aprobaciones_almacen`
--
ALTER TABLE `aprobaciones_almacen`
  ADD PRIMARY KEY (`id_aprobacion`),
  ADD KEY `id_producto` (`id_producto`),
  ADD KEY `usuario_solicita` (`usuario_solicita`),
  ADD KEY `usuario_responde` (`usuario_responde`);

--
-- Indices de la tabla `categorias`
--
ALTER TABLE `categorias`
  ADD PRIMARY KEY (`id_categoria`);

--
-- Indices de la tabla `detalle_ventas`
--
ALTER TABLE `detalle_ventas`
  ADD PRIMARY KEY (`id_detalle`),
  ADD KEY `venta_id` (`venta_id`),
  ADD KEY `producto_id` (`producto_id`);

--
-- Indices de la tabla `historial_acciones`
--
ALTER TABLE `historial_acciones`
  ADD PRIMARY KEY (`id_historial`),
  ADD KEY `usuario_id` (`usuario_id`);

--
-- Indices de la tabla `productos`
--
ALTER TABLE `productos`
  ADD PRIMARY KEY (`id_producto`),
  ADD KEY `categoria_id` (`categoria_id`);

--
-- Indices de la tabla `recuperacion_password`
--
ALTER TABLE `recuperacion_password`
  ADD PRIMARY KEY (`id`),
  ADD KEY `usuario_id` (`usuario_id`);

--
-- Indices de la tabla `respaldos`
--
ALTER TABLE `respaldos`
  ADD PRIMARY KEY (`id_respaldo`),
  ADD KEY `usuario_id` (`usuario_id`);

--
-- Indices de la tabla `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`id_rol`);

--
-- Indices de la tabla `solicitudes_devoluciones`
--
ALTER TABLE `solicitudes_devoluciones`
  ADD PRIMARY KEY (`id_solicitud`),
  ADD KEY `id_venta` (`id_venta`),
  ADD KEY `id_producto` (`id_producto`),
  ADD KEY `usuario_solicita` (`usuario_solicita`),
  ADD KEY `usuario_responde` (`usuario_responde`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`id_usuario`),
  ADD UNIQUE KEY `correo` (`correo`),
  ADD KEY `rol_id` (`rol_id`);

--
-- Indices de la tabla `ventas`
--
ALTER TABLE `ventas`
  ADD PRIMARY KEY (`id_venta`),
  ADD KEY `usuario_id` (`usuario_id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `aprobaciones`
--
ALTER TABLE `aprobaciones`
  MODIFY `id_aprobacion` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT de la tabla `aprobaciones_almacen`
--
ALTER TABLE `aprobaciones_almacen`
  MODIFY `id_aprobacion` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT de la tabla `categorias`
--
ALTER TABLE `categorias`
  MODIFY `id_categoria` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `detalle_ventas`
--
ALTER TABLE `detalle_ventas`
  MODIFY `id_detalle` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `historial_acciones`
--
ALTER TABLE `historial_acciones`
  MODIFY `id_historial` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=218;

--
-- AUTO_INCREMENT de la tabla `productos`
--
ALTER TABLE `productos`
  MODIFY `id_producto` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `recuperacion_password`
--
ALTER TABLE `recuperacion_password`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `respaldos`
--
ALTER TABLE `respaldos`
  MODIFY `id_respaldo` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `roles`
--
ALTER TABLE `roles`
  MODIFY `id_rol` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `solicitudes_devoluciones`
--
ALTER TABLE `solicitudes_devoluciones`
  MODIFY `id_solicitud` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `id_usuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT de la tabla `ventas`
--
ALTER TABLE `ventas`
  MODIFY `id_venta` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `aprobaciones`
--
ALTER TABLE `aprobaciones`
  ADD CONSTRAINT `aprobaciones_ibfk_1` FOREIGN KEY (`usuario_id`) REFERENCES `usuarios` (`id_usuario`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
