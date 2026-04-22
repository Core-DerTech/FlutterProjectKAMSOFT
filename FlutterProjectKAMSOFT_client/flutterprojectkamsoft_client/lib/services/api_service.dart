import 'dart:convert';
import '../models/patient.dart';
import 'package:http/http.dart' as http;

class ApiService {
  static const String baseUrl = 'http://localhost:5150';
  static const String patientsEndpoint = '$baseUrl/api/PatientAppoinment/create';
  static const String appointmentsEndpoint = '$baseUrl/appointments';

  Future<List<Patient>> getPatients() async {
    try {
      final response = await http.get(Uri.parse(patientsEndpoint));

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
}