import 'dart:convert';

import 'package:http/http.dart' as http;

import '../contratos/modelos.dart';

/// URL base de la API. Por defecto apunta al emulador de Android
/// (10.0.2.2 es el "localhost" del equipo anfitrión visto desde el emulador).
/// Se puede cambiar en el build con:
/// `flutter build apk --dart-define=API_URL=https://mathhammer-api.onrender.com`
const String urlBase = String.fromEnvironment('API_URL', defaultValue: 'http://10.0.2.2:5188');

/// Envía la petición de combate al backend y devuelve el resultado.
/// Lanza una [ApiException] descriptiva cuando la petición falla.
class ClienteApi {
  ClienteApi({http.Client? cliente}) : _cliente = cliente ?? http.Client();

  final http.Client _cliente;

  Future<ResultadoCombate> simularCombate(PeticionCombate peticion) async {
    final respuesta = await _cliente.post(
      Uri.parse('$urlBase/api/combate/simular'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode(peticion.toJson()),
    );

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