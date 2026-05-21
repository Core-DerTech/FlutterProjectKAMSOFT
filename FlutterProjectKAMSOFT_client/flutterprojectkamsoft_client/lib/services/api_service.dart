import 'dart:convert';
import '../models/patient.dart';
import 'package:http/http.dart' as http;

class ApiService {
  static const String baseUrl = 'http://localhost:5150';
  static const Map<String, String> encryptionQuery = {
    'CipherType': '2',
  };

  Future<List<Patient>> getPatients() async {
    try {
      final uri = Uri.parse('$baseUrl/api/PatientAppoinment/get-patient-data')
          .replace(queryParameters: encryptionQuery);
      final response = await http.get(uri);

      if (response.statusCode == 200) {
        final List<dynamic> data = jsonDecode(response.body);
        
        return data
            .map((json) => Patient.fromJson(json))
            .toList();
      } else if (response.statusCode == 404) {
        throw Exception('Endpoint Patients not found');
      } else {
        throw Exception('Failed to load patients: ${response.statusCode}');
      }
    } catch (e) {
      print('Error fetching patients: $e');
      rethrow;
    }
  }

  Future<Patient> createAppointment({
    required String firstName,
    required String lastName,
    required int pessel,
    required DateTime dateOfBirth,
    required int disease,
    required String appointmentTitle,
    required String appointmentDescription,
  }) async {
    final uri = Uri.parse('$baseUrl/api/PatientAppoinment/create')
        .replace(queryParameters: encryptionQuery);
    final response = await http.post(
      uri,
      headers: const {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: jsonEncode({
        'firstName': firstName,
        'lastName': lastName,
        'pessel': pessel,
        'dateOfBirth': dateOfBirth.toIso8601String().split('T').first,
        'disease': disease,
        'appointmentTitle': appointmentTitle,
        'appointmentDescription': appointmentDescription,
      }),
    );

    if (response.statusCode == 200) {
      return Patient.fromJson(jsonDecode(response.body) as Map<String, dynamic>);
    }

    throw Exception('Failed to create appointment: ${response.statusCode} ${response.body}');
  }
}
