import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:mathhammer_app/pantallas/pantalla_combate.dart';

void main() {
  testWidgets('la pantalla de combate se renderiza con sus secciones', (tester) async {
    await tester.pumpWidget(const MaterialApp(home: PantallaCombate()));

    expect(find.text('Math'), findsOneWidget);
    expect(find.text('Hammer'), findsOneWidget);
    expect(find.text('ATACANTE'), findsOneWidget);
    expect(find.text('DEFENSOR'), findsOneWidget);
    expect(find.text('CALCULAR COMBATE'), findsOneWidget);
  });
}