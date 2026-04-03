import 'dart:convert';
import 'dart:io'; // Для перевірки платформи
import '../models/patient.dart';
import 'package:http/http.dart' as http;

class ApiService {
  // 1. ВИПРАВЛЕННЯ: Для Android-емулятора використовуємо 10.0.2.2 замість localhost
  // Також додаємо /api/, бо в контролері вказано [Route("api/[controller]")]
  static String get baseUrl {
    if (Platform.isAndroid) return 'http://10.0.2.2:5150/api';
    return 'http://localhost:5150/api';
  }

  static String get patientEndPoint => '$baseUrl/PatientAppoinment/get-patient-data';

  Future<List<Patient>> getPatients() async {
    try {
      // 2. ВИПРАВЛЕННЯ: Додаємо pessel як query-параметр, бо в C# він [FromQuery]
      final url = Uri.parse(patientEndPoint).replace(queryParameters: {
        'pessel': '12345678901', 
      });

      final response = await http.get(url);

      if (response.statusCode == 200) {
        final List<dynamic> jsonData = jsonDecode(response.body);
        return jsonData.map((json) => Patient.fromJson(json)).toList();
      } else {
        print('Server Error: ${response.statusCode} - ${response.body}');
        throw Exception("Failed to fetch patients: ${response.statusCode}");
      }
    } catch (e) {
      print('Network Error: $e');
      rethrow;
    }
  }
}