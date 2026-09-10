import 'dart:convert';

import 'package:flutter_test/flutter_test.dart';
import 'package:http/http.dart' as http;
import 'package:http/testing.dart';
import 'package:mathhammer_app/contratos/modelos.dart';
import 'package:mathhammer_app/servicios/cliente_api.dart';

void main() {
  const peticion = PeticionCombate(
    atacante: Atacante(nombreUnidad: 'Intercesores', impactaA: 3),
    arma: Arma(cantidadAtaques: 8, fuerza: 5, penetracionArmadura: -2, danio: 1),
    defensor: Defensor(
      nombreUnidad: 'Necrones',
      resistencia: 4,
      salvacion: 3,
      heridasPorMiniatura: 2,
      cantidadMiniaturas: 5,
    ),
    iteraciones: 10000,
  );

  group('ClienteApi', () {
    test('serializa la petición con las claves del contrato', () {
      final json = peticion.toJson();

      expect((json['atacante'] as Map<String, dynamic>)['impactaA'], 3);
      expect((json['arma'] as Map<String, dynamic>)['penetracionArmadura'], -2);
      expect((json['defensor'] as Map<String, dynamic>)['salvacion'], 3);
      expect(
        (json['configuracionSimulacion'] as Map<String, dynamic>)['iteraciones'],
        10000,
      );
    });

    test('parsea un 200 en ResultadoCombate', () async {
      final cliente = ClienteApi(
        cliente: MockClient((solicitud) async {
          expect(solicitud.url.path, '/api/combate/simular');
          return http.Response(
            jsonEncode({
              'metricas': {
                'impactosEsperados': 5.333,
                'heridasEsperadas': 3.556,
                'salvacionesEnemigo': 1.185,
                'probabilidadMatarUnidad': 0.31,
                'miniaturasEliminadas': 0.935,
                'danioMedioEsperado': 2.377,
                'percentil25': 1.0,
                'percentil75': 3.0,
              },
              'resumen': {'iteracionesEjecutadas': 10000, 'duracionMilisegundos': 14},
            }),
            200,
            headers: {'content-type': 'application/json'},
          );
        }),
      );

      final resultado = await cliente.simularCombate(peticion);

      expect(resultado.metricas.danioMedioEsperado, 2.377);
      expect(resultado.metricas.probabilidadMatarUnidad, 0.31);
      expect(resultado.iteracionesEjecutadas, 10000);
    });

    test('lanza ApiException con el detalle en un 422', () async {
      final cliente = ClienteApi(
        cliente: MockClient((solicitud) async {
          return http.Response(
            jsonEncode({'detail': 'La petición no es válida.'}),
            422,
            headers: {'content-type': 'application/problem+json'},
          );
        }),
      );

      await expectLater(
        cliente.simularCombate(peticion),
        throwsA(isA<ApiException>().having((e) => e.mensaje, 'mensaje', contains('no es válida'))),
      );
    });
  });
}