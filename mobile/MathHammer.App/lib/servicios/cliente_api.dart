import 'dart:async';
import 'dart:convert';

import 'package:http/http.dart' as http;

import '../contratos/modelos.dart';

/// URL base de la API. Por defecto apunta a la API desplegada en Render para
/// que funcione en un móvil real sin configuración.
///
/// Para desarrollo local (emulador de Android) se puede sobrescribir con:
/// `flutter build apk --dart-define=API_URL=http://10.0.2.2:5188`
const String urlBase = String.fromEnvironment(
  'API_URL',
  defaultValue: 'https://mathhammer-api.onrender.com',
);

const Duration _tiempoEspera = Duration(seconds: 30);

/// Envía la petición de combate al backend y devuelve el resultado.
/// Lanza una [ApiException] descriptiva cuando la petición falla.
class ClienteApi {
  ClienteApi({http.Client? cliente}) : _cliente = cliente ?? http.Client();

  final http.Client _cliente;

  Future<ResultadoCombate> simularCombate(PeticionCombate peticion) async {
    final http.Response respuesta;
    try {
      respuesta = await _cliente
          .post(
            Uri.parse('$urlBase/api/combate/simular'),
            headers: {'Content-Type': 'application/json'},
            body: jsonEncode(peticion.toJson()),
          )
          .timeout(_tiempoEspera);
    } on TimeoutException {
      throw ApiException('El servidor tardó demasiado en responder ($urlBase).');
    } catch (_) {
      throw ApiException('No se pudo conectar con el servidor ($urlBase). Comprueba tu conexión.');
    }

    if (respuesta.statusCode == 200) {
      return ResultadoCombate.fromJson(jsonDecode(respuesta.body) as Map<String, dynamic>);
    }

    if (respuesta.statusCode == 422) {
      final problema = jsonDecode(respuesta.body) as Map<String, dynamic>;
      throw ApiException(problema['detail']?.toString() ?? 'La petición no es válida.');
    }

    throw ApiException('Error inesperado del servidor (${respuesta.statusCode}).');
  }
}

class ApiException implements Exception {
  const ApiException(this.mensaje);

  final String mensaje;

  @override
  String toString() => mensaje;
}