import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/patient_result.dart';

class MedicalRepository {
  final String apiUrl = "http://localhost:5150/api/medicalresults/dashboard";
  
  Future<List<PatientResult>> fetchDashboardData() async {
    final response = await http.get(Uri.parse(apiUrl));

    if (response.statusCode == 200) {
      List<dynamic> body = jsonDecode(response.body);
      return body.map((item) => PatientResult.fromJson(item)).toList();
    } else {
      throw Exception("Backend returned error: ${response.statusCode}");
    }
  }
}