﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CongThongTinSV
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MoodleEntities : DbContext
    {
        public MoodleEntities()
            : base("name=MoodleEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<fit_course> fit_course { get; set; }
        public DbSet<fit_enrol> fit_enrol { get; set; }
        public DbSet<fit_external_services> fit_external_services { get; set; }
        public DbSet<fit_grade_grades> fit_grade_grades { get; set; }
        public DbSet<fit_grade_grades_history> fit_grade_grades_history { get; set; }
        public DbSet<fit_grade_import_newitem> fit_grade_import_newitem { get; set; }
        public DbSet<fit_grade_import_values> fit_grade_import_values { get; set; }
        public DbSet<fit_grade_items> fit_grade_items { get; set; }
        public DbSet<fit_grade_items_history> fit_grade_items_history { get; set; }
        public DbSet<fit_grade_letters> fit_grade_letters { get; set; }
        public DbSet<fit_grade_outcomes> fit_grade_outcomes { get; set; }
        public DbSet<fit_grade_outcomes_courses> fit_grade_outcomes_courses { get; set; }
        public DbSet<fit_grade_outcomes_history> fit_grade_outcomes_history { get; set; }
        public DbSet<fit_grade_settings> fit_grade_settings { get; set; }
        public DbSet<fit_grading_areas> fit_grading_areas { get; set; }
        public DbSet<fit_grading_definitions> fit_grading_definitions { get; set; }
        public DbSet<fit_grading_instances> fit_grading_instances { get; set; }
        public DbSet<fit_message> fit_message { get; set; }
        public DbSet<fit_message_contacts> fit_message_contacts { get; set; }
        public DbSet<fit_message_read> fit_message_read { get; set; }
        public DbSet<fit_message_working> fit_message_working { get; set; }
        public DbSet<fit_user> fit_user { get; set; }
        public DbSet<fit_user_enrolments> fit_user_enrolments { get; set; }
        public DbSet<fit_role> fit_role { get; set; }
        public DbSet<fit_role_context_levels> fit_role_context_levels { get; set; }
        public DbSet<fit_role_names> fit_role_names { get; set; }
        public DbSet<fit_context> fit_context { get; set; }
        public DbSet<fit_quiz> fit_quiz { get; set; }
        public DbSet<fit_quiz_attempts> fit_quiz_attempts { get; set; }
        public DbSet<fit_quiz_grades> fit_quiz_grades { get; set; }
        public DbSet<fit_quiz_overrides> fit_quiz_overrides { get; set; }
        public DbSet<fit_quiz_overview_regrades> fit_quiz_overview_regrades { get; set; }
        public DbSet<fit_quiz_question_instances> fit_quiz_question_instances { get; set; }
        public DbSet<fit_quiz_question_response_stats> fit_quiz_question_response_stats { get; set; }
        public DbSet<fit_quiz_question_statistics> fit_quiz_question_statistics { get; set; }
        public DbSet<fit_quiz_reports> fit_quiz_reports { get; set; }
        public DbSet<fit_quiz_statistics> fit_quiz_statistics { get; set; }
        public DbSet<fit_role_assignments> fit_role_assignments { get; set; }
        public DbSet<fit_user_info_data> fit_user_info_data { get; set; }
        public DbSet<fit_user_info_field> fit_user_info_field { get; set; }
    }
}
